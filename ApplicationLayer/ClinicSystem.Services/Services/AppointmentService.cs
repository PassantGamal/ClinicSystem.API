using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Enums;
using ClinicSystem.ServicesAbstration.Contracts.Repositories;
using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.AppointmentDTOS;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Services.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AppointmentDTO>> GetAppointmentByIdAsync(Guid id)
        {
            var appointments = await _unitOfWork.Repository<Appointment>()
                .FindAsync(a => a.Id == id,
                    q => q.Include(a => a.Doctor),
                    q => q.Include(a => a.Patient));

            var appointment = appointments.FirstOrDefault();
            if (appointment is null)
                return Result<AppointmentDTO>.Fail(Error.NotFound("Appointment.NotFound", "Appointment not found"));

            return Result<AppointmentDTO>.Ok(MapToDTO(appointment));
        }

        public async Task<Result<IEnumerable<AppointmentDTO>>> GetAllAppointmentsAsync()
        {
            var appointments = await _unitOfWork.Repository<Appointment>()
                .FindAsync(a => true,
                    q => q.Include(a => a.Doctor),
                    q => q.Include(a => a.Patient));

            return Result<IEnumerable<AppointmentDTO>>.Ok(appointments.Select(MapToDTO));
        }

        public async Task<Result<IEnumerable<AppointmentDTO>>> GetDoctorAppointmentsAsync(Guid doctorId)
        {
            var appointments = await _unitOfWork.Repository<Appointment>()
                .FindAsync(a => a.DoctorId == doctorId,
                    q => q.Include(a => a.Doctor),
                    q => q.Include(a => a.Patient));

            return Result<IEnumerable<AppointmentDTO>>.Ok(appointments.Select(MapToDTO));
        }

        public async Task<Result<IEnumerable<AppointmentDTO>>> GetPatientAppointmentsAsync(Guid patientId)
        {
            var appointments = await _unitOfWork.Repository<Appointment>()
                .FindAsync(a => a.PatientId == patientId,
                    q => q.Include(a => a.Doctor),
                    q => q.Include(a => a.Patient));

            return Result<IEnumerable<AppointmentDTO>>.Ok(appointments.Select(MapToDTO));
        }

        public async Task<Result<AppointmentDTO>> BookAppointmentAsync(BookAppointmentDTO dto)
        {
            var doctors = await _unitOfWork.Repository<Doctor>().FindAsync(d => d.Email == dto.DoctorEmail);
            var doctor = doctors.FirstOrDefault();

            if (doctor is null)
                return Result<AppointmentDTO>.Fail(Error.NotFound("Doctor.NotFound", "Doctor not found"));

            var patients = await _unitOfWork.Repository<Patient>().FindAsync(p => p.Email == dto.PatientEmail);
            var patient = patients.FirstOrDefault();

            if (patient is null)
                return Result<AppointmentDTO>.Fail(Error.NotFound("Patient.NotFound", "Patient not found"));

            var appointment = new Appointment
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                AppointmentDate = dto.AppointmentDate,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = AppointmentStatus.Pending,
                Reason = dto.Reason,
                Notes = dto.Notes
            };

            await _unitOfWork.Repository<Appointment>().AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            // ✅ إنشاء Invoice تلقائي
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                AppointmentId = appointment.Id,
                PatientId = patient.Id,
                Amount = doctor.ConsultationFee,
                PaidAmount = 0,
                PaymentStatus = PaymentStatus.Pending,
                InvoiceDate = DateTime.UtcNow,
                PaymentMethod = null
            };

            await _unitOfWork.Repository<Invoice>().AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync();

            var createdAppointment = await _unitOfWork.Repository<Appointment>()
                .FindAsync(a => a.Id == appointment.Id,
                    q => q.Include(a => a.Doctor),
                    q => q.Include(a => a.Patient));

            return Result<AppointmentDTO>.Ok(MapToDTO(createdAppointment.FirstOrDefault()!));
        }

        public async Task<Result<AppointmentDTO>> UpdateAppointmentAsync(Guid id, BookAppointmentDTO dto)
        {
            var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);
            if (appointment is null)
                return Result<AppointmentDTO>.Fail(Error.NotFound("Appointment.NotFound", "Appointment not found"));

            appointment.AppointmentDate = dto.AppointmentDate;
            appointment.StartTime = dto.StartTime;
            appointment.EndTime = dto.EndTime;
            appointment.Reason = dto.Reason;
            appointment.Notes = dto.Notes;
            appointment.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Appointment>().Update(appointment);
            await _unitOfWork.SaveChangesAsync();

            var updatedAppointment = await _unitOfWork.Repository<Appointment>()
                .FindAsync(a => a.Id == id,
                    q => q.Include(a => a.Doctor),
                    q => q.Include(a => a.Patient));

            return Result<AppointmentDTO>.Ok(MapToDTO(updatedAppointment.FirstOrDefault()!));
        }

        public async Task<Result> CancelAppointmentAsync(Guid id)
        {
            var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);
            if (appointment is null)
                return Result.Fail(Error.NotFound("Appointment.NotFound", "Appointment not found"));

            appointment.Status = AppointmentStatus.Cancelled;
            appointment.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Appointment>().Update(appointment);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result> CompleteAppointmentAsync(Guid id)
        {
            var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);
            if (appointment is null)
                return Result.Fail(Error.NotFound("Appointment.NotFound", "Appointment not found"));

            appointment.Status = AppointmentStatus.Completed;
            appointment.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Appointment>().Update(appointment);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        private AppointmentDTO MapToDTO(Appointment appointment)
        {
            return new AppointmentDTO
            {
                Id = appointment.Id,
                DoctorId = appointment.DoctorId,
                DoctorName = appointment.Doctor?.FullName ?? "Unknown",
                PatientId = appointment.PatientId,
                PatientName = appointment.Patient?.FullName ?? "Unknown",
                AppointmentDate = appointment.AppointmentDate,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Status = appointment.Status.ToString(),
                Reason = appointment.Reason,
                Notes = appointment.Notes
            };
        }
    }
}