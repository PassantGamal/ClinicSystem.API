using ClinicSystem.Domain.Entities;
using ClinicSystem.ServicesAbstration.Contracts.Repositories;
using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.PatientDTOS;
using ClinicSystem.Shared.DataTransferObject.AppointmentDTOS;
using ClinicSystem.Shared.DataTransferObject.PrescriptionDTOS;
using ClinicSystem.Shared.DataTransferObject.MedicalHistoryDTOS;

namespace ClinicSystem.Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PatientDTO>> GetPatientByIdAsync(Guid id)
        {
            var patient = await _unitOfWork.Repository<Patient>().GetByIdAsync(id);
            if (patient is null)
                return Result<PatientDTO>.Fail(Error.NotFound("Patient.NotFound", "Patient not found"));

            return Result<PatientDTO>.Ok(MapToDTO(patient));
        }

        public async Task<Result<IEnumerable<PatientDTO>>> GetAllPatientsAsync()
        {
            var patients = await _unitOfWork.Repository<Patient>().GetAllAsync();
            return Result<IEnumerable<PatientDTO>>.Ok(patients.Select(MapToDTO));
        }

        public async Task<Result<PatientDTO>> CreatePatientAsync(AddPatientDTO dto)
        {
            var existingPatient = await _unitOfWork.Repository<Patient>()
                .FindAsync(p => p.Email == dto.Email);

            if (existingPatient.Any())
                return Result<PatientDTO>.Fail(Error.Validation("Patient.EmailExists", "Email already exists"));

            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                BloodType = dto.BloodType,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                EmergencyContact = dto.EmergencyContact
            };

            await _unitOfWork.Repository<Patient>().AddAsync(patient);
            await _unitOfWork.SaveChangesAsync();

            return Result<PatientDTO>.Ok(MapToDTO(patient));
        }

        public async Task<Result<PatientDTO>> UpdatePatientAsync(Guid id, AddPatientDTO dto)
        {
            var patient = await _unitOfWork.Repository<Patient>().GetByIdAsync(id);
            if (patient is null)
                return Result<PatientDTO>.Fail(Error.NotFound("Patient.NotFound", "Patient not found"));

            patient.FullName = dto.FullName;
            patient.DateOfBirth = dto.DateOfBirth;
            patient.Gender = dto.Gender;
            patient.BloodType = dto.BloodType;
            patient.Phone = dto.Phone;
            patient.Email = dto.Email;
            patient.Address = dto.Address;
            patient.EmergencyContact = dto.EmergencyContact;
            patient.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Patient>().Update(patient);
            await _unitOfWork.SaveChangesAsync();

            return Result<PatientDTO>.Ok(MapToDTO(patient));
        }

        public async Task<Result> DeletePatientAsync(Guid id)
        {
            var patient = await _unitOfWork.Repository<Patient>().GetByIdAsync(id);
            if (patient is null)
                return Result.Fail(Error.NotFound("Patient.NotFound", "Patient not found"));

            patient.IsDeleted = true;
            patient.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Patient>().Update(patient);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result<MedicalHistoryDTO>> GetPatientMedicalHistoryAsync(Guid patientId)
        {
            var patient = await _unitOfWork.Repository<Patient>().GetByIdAsync(patientId);
            if (patient is null)
                return Result<MedicalHistoryDTO>.Fail(Error.NotFound("Patient.NotFound", "Patient not found"));

            var history = await _unitOfWork.Repository<MedicalHistory>()
                .FindAsync(mh => mh.PatientId == patientId);

            var medicalHistory = history.FirstOrDefault();
            if (medicalHistory is null)
                return Result<MedicalHistoryDTO>.Fail(Error.NotFound("MedicalHistory.NotFound", "Medical history not found"));

            return Result<MedicalHistoryDTO>.Ok(new MedicalHistoryDTO
            {
                Id = medicalHistory.Id,
                PatientId = medicalHistory.PatientId,
                Allergies = medicalHistory.Allergies,
                ChronicConditions = medicalHistory.ChronicConditions,
                PreviousSurgeries = medicalHistory.PreviousSurgeries,
                CurrentMedications = medicalHistory.CurrentMedications,
                FamilyHistory = medicalHistory.FamilyHistory,
                Notes = medicalHistory.Notes
            });
        }

        public async Task<Result<IEnumerable<AppointmentDTO>>> GetPatientAppointmentsAsync(Guid patientId)
        {
            var appointments = await _unitOfWork.Repository<Appointment>()
                .FindAsync(a => a.PatientId == patientId);

            return Result<IEnumerable<AppointmentDTO>>.Ok(appointments.Select(a => new AppointmentDTO
            {
                Id = a.Id,
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                AppointmentDate = a.AppointmentDate,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                Status = a.Status.ToString(),
                Reason = a.Reason,
                Notes = a.Notes
            }));
        }

        public async Task<Result<IEnumerable<PrescriptionDTO>>> GetPatientPrescriptionsAsync(Guid patientId)
        {
            var prescriptions = await _unitOfWork.Repository<Prescription>()
                .FindAsync(p => p.PatientId == patientId);

            return Result<IEnumerable<PrescriptionDTO>>.Ok(prescriptions.Select(p => new PrescriptionDTO
            {
                Id = p.Id,
                DoctorId = p.DoctorId,
                PatientId = p.PatientId,
                Diagnosis = p.Diagnosis,
                PrescriptionDate = p.PrescriptionDate,
                Notes = p.Notes
            }));
        }

        private PatientDTO MapToDTO(Patient patient) => new PatientDTO
        {
            Id = patient.Id,
            FullName = patient.FullName,
            DateOfBirth = patient.DateOfBirth,
            Gender = patient.Gender,
            BloodType = patient.BloodType,
            Phone = patient.Phone,
            Email = patient.Email,
            Address = patient.Address,
            EmergencyContact = patient.EmergencyContact
        };
    }
}