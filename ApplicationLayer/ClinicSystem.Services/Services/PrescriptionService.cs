using ClinicSystem.Domain.Entities;
using ClinicSystem.ServicesAbstration.Contracts.Repositories;
using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.PrescriptionDTOS;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Services.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PrescriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PrescriptionDTO>> GetPrescriptionByIdAsync(Guid id)
        {
            var prescriptions = await _unitOfWork.Repository<Prescription>()
                .FindAsync(p => p.Id == id,
                    q => q.Include(p => p.Doctor),
                    q => q.Include(p => p.Patient),
                    q => q.Include(p => p.PrescriptionMedicines).ThenInclude(pm => pm.Medicine));

            var prescription = prescriptions.FirstOrDefault();

            if (prescription is null)
                return Result<PrescriptionDTO>.Fail(Error.NotFound("Prescription.NotFound", "Prescription not found"));

            return Result<PrescriptionDTO>.Ok(MapToDTO(prescription));
        }

        public async Task<Result<IEnumerable<PrescriptionDTO>>> GetAllPrescriptionsAsync()
        {
            var prescriptions = await _unitOfWork.Repository<Prescription>()
                .FindAsync(p => true,
                    q => q.Include(p => p.Doctor),
                    q => q.Include(p => p.Patient),
                    q => q.Include(p => p.PrescriptionMedicines).ThenInclude(pm => pm.Medicine));

            return Result<IEnumerable<PrescriptionDTO>>.Ok(prescriptions.Select(MapToDTO));
        }

        public async Task<Result<IEnumerable<PrescriptionDTO>>> GetDoctorPrescriptionsAsync(Guid doctorId)
        {
            var prescriptions = await _unitOfWork.Repository<Prescription>()
                .FindAsync(p => p.DoctorId == doctorId,
                    q => q.Include(p => p.Doctor),
                    q => q.Include(p => p.Patient),
                    q => q.Include(p => p.PrescriptionMedicines).ThenInclude(pm => pm.Medicine));

            return Result<IEnumerable<PrescriptionDTO>>.Ok(prescriptions.Select(MapToDTO));
        }

        public async Task<Result<IEnumerable<PrescriptionDTO>>> GetPatientPrescriptionsAsync(Guid patientId)
        {
            var prescriptions = await _unitOfWork.Repository<Prescription>()
                .FindAsync(p => p.PatientId == patientId,
                    q => q.Include(p => p.Doctor),
                    q => q.Include(p => p.Patient),
                    q => q.Include(p => p.PrescriptionMedicines).ThenInclude(pm => pm.Medicine));

            return Result<IEnumerable<PrescriptionDTO>>.Ok(prescriptions.Select(MapToDTO));
        }

        public async Task<Result<PrescriptionDTO>> CreatePrescriptionAsync(CreatePrescriptionDTO dto)
        {
            var doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(dto.DoctorId);
            if (doctor is null)
                return Result<PrescriptionDTO>.Fail(Error.NotFound("Doctor.NotFound", "Doctor not found"));

            var patient = await _unitOfWork.Repository<Patient>().GetByIdAsync(dto.PatientId);
            if (patient is null)
                return Result<PrescriptionDTO>.Fail(Error.NotFound("Patient.NotFound", "Patient not found"));

            var prescription = new Prescription
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Diagnosis = dto.Diagnosis,
                Notes = dto.Notes,
                PrescriptionMedicines = dto.Medicines.Select(m => new PrescriptionMedicine
                {
                    MedicineId = m.MedicineId,
                    Dosage = m.Dosage,
                    Frequency = m.Frequency,
                    DurationDays = m.DurationDays,
                    Quantity = m.Quantity,
                    Instructions = m.Instructions
                }).ToList()
            };

            await _unitOfWork.Repository<Prescription>().AddAsync(prescription);
            await _unitOfWork.SaveChangesAsync();

            var createdPrescription = await _unitOfWork.Repository<Prescription>()
                .FindAsync(p => p.Id == prescription.Id,
                    q => q.Include(p => p.Doctor),
                    q => q.Include(p => p.Patient),
                    q => q.Include(p => p.PrescriptionMedicines).ThenInclude(pm => pm.Medicine));

            return Result<PrescriptionDTO>.Ok(MapToDTO(createdPrescription.FirstOrDefault()!));
        }

        public async Task<Result<PrescriptionDTO>> UpdatePrescriptionAsync(Guid id, CreatePrescriptionDTO dto)
        {
            var prescription = await _unitOfWork.Repository<Prescription>().GetByIdAsync(id);
            if (prescription is null)
                return Result<PrescriptionDTO>.Fail(Error.NotFound("Prescription.NotFound", "Prescription not found"));

            prescription.Diagnosis = dto.Diagnosis;
            prescription.Notes = dto.Notes;
            prescription.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Prescription>().Update(prescription);
            await _unitOfWork.SaveChangesAsync();

            var updatedPrescription = await _unitOfWork.Repository<Prescription>()
                .FindAsync(p => p.Id == id,
                    q => q.Include(p => p.Doctor),
                    q => q.Include(p => p.Patient),
                    q => q.Include(p => p.PrescriptionMedicines).ThenInclude(pm => pm.Medicine));

            return Result<PrescriptionDTO>.Ok(MapToDTO(updatedPrescription.FirstOrDefault()!));
        }

        public async Task<Result> DeletePrescriptionAsync(Guid id)
        {
            var prescription = await _unitOfWork.Repository<Prescription>().GetByIdAsync(id);
            if (prescription is null)
                return Result.Fail(Error.NotFound("Prescription.NotFound", "Prescription not found"));

            prescription.IsDeleted = true;
            prescription.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Prescription>().Update(prescription);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        private PrescriptionDTO MapToDTO(Prescription prescription)
        {
            return new PrescriptionDTO
            {
                Id = prescription.Id,
                DoctorId = prescription.DoctorId,
                DoctorName = prescription.Doctor?.FullName ?? "Unknown",
                PatientId = prescription.PatientId,
                PatientName = prescription.Patient?.FullName ?? "Unknown",
                Diagnosis = prescription.Diagnosis,
                PrescriptionDate = prescription.PrescriptionDate,
                Notes = prescription.Notes,
                Medicines = prescription.PrescriptionMedicines?
                    .Select(pm => new PrescriptionMedicineDTO
                    {
                        MedicineId = pm.MedicineId,
                        MedicineName = pm.Medicine?.Name ?? "Unknown",
                        Dosage = pm.Dosage,
                        Frequency = pm.Frequency,
                        DurationDays = pm.DurationDays,
                        Quantity = pm.Quantity,
                        Instructions = pm.Instructions
                    }).ToList() ?? new()
            };
        }
    }
}