using ClinicSystem.Domain.Entities;
using ClinicSystem.ServicesAbstration.Contracts.Repositories;
using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.DoctorDTOS;

namespace ClinicSystem.Services.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DoctorDTO>> GetDoctorByIdAsync(Guid id)
        {
            var doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(id);
            if (doctor is null)
                return Result<DoctorDTO>.Fail(Error.NotFound("Doctor.NotFound", "Doctor not found"));

            return Result<DoctorDTO>.Ok(MapToDTO(doctor));
        }

        public async Task<Result<IEnumerable<DoctorDTO>>> GetAllDoctorsAsync()
        {
            var doctors = await _unitOfWork.Repository<Doctor>().GetAllAsync();
            return Result<IEnumerable<DoctorDTO>>.Ok(doctors.Select(MapToDTO));
        }

        public async Task<Result<IEnumerable<DoctorDTO>>> GetDoctorsBySpecializationAsync(string specialization)
        {
            var doctors = await _unitOfWork.Repository<Doctor>()
                .FindAsync(d => d.Specialization == specialization);
            return Result<IEnumerable<DoctorDTO>>.Ok(doctors.Select(MapToDTO));
        }

        public async Task<Result<DoctorDTO>> CreateDoctorAsync(AddDoctorDTO dto)
        {
            var existing = await _unitOfWork.Repository<Doctor>()
                .FindAsync(d => d.LicenseNumber == dto.LicenseNumber);
            if (existing.Any())
                return Result<DoctorDTO>.Fail(Error.Validation("Doctor.LicenseExists", "License number already exists"));

            var doctor = new Doctor
            {
                FullName = dto.FullName,
                Specialization = dto.Specialization,
                LicenseNumber = dto.LicenseNumber,
                Phone = dto.Phone,
                Email = dto.Email,
                Bio = dto.Bio,
                ConsultationFee = dto.ConsultationFee
            };

            await _unitOfWork.Repository<Doctor>().AddAsync(doctor);
            await _unitOfWork.SaveChangesAsync();

            return Result<DoctorDTO>.Ok(MapToDTO(doctor));
        }

        public async Task<Result<DoctorDTO>> UpdateDoctorAsync(Guid id, AddDoctorDTO dto)
        {
            var doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(id);
            if (doctor is null)
                return Result<DoctorDTO>.Fail(Error.NotFound("Doctor.NotFound", "Doctor not found"));

            doctor.FullName = dto.FullName;
            doctor.Specialization = dto.Specialization;
            doctor.LicenseNumber = dto.LicenseNumber;
            doctor.Phone = dto.Phone;
            doctor.Email = dto.Email;
            doctor.Bio = dto.Bio;
            doctor.ConsultationFee = dto.ConsultationFee;
            doctor.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Doctor>().Update(doctor);
            await _unitOfWork.SaveChangesAsync();

            return Result<DoctorDTO>.Ok(MapToDTO(doctor));
        }

        public async Task<Result> DeleteDoctorAsync(Guid id)
        {
            var doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(id);
            if (doctor is null)
                return Result.Fail(Error.NotFound("Doctor.NotFound", "Doctor not found"));

            doctor.IsDeleted = true;
            doctor.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Doctor>().Update(doctor);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result<IEnumerable<DoctorScheduleDTO>>> GetDoctorScheduleAsync(Guid doctorId)
        {
            var doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(doctorId);
            if (doctor is null)
                return Result<IEnumerable<DoctorScheduleDTO>>.Fail(Error.NotFound("Doctor.NotFound", "Doctor not found"));

            var schedules = await _unitOfWork.Repository<DoctorSchedule>()
                .FindAsync(ds => ds.DoctorId == doctorId);

            return Result<IEnumerable<DoctorScheduleDTO>>.Ok(schedules.Select(ds => new DoctorScheduleDTO
            {
                Id = ds.Id,
                DayOfWeek = ds.DayOfWeek,
                StartTime = ds.StartTime,
                EndTime = ds.EndTime,
                SlotDuration = ds.SlotDuration,
                IsAvailable = ds.IsAvailable
            }));
        }

        public async Task<Result<IEnumerable<TimeSlotDTO>>> GetAvailableSlotsAsync(Guid doctorId, DateTime date)
        {
            var schedules = await _unitOfWork.Repository<DoctorSchedule>()
                .FindAsync(ds => ds.DoctorId == doctorId && ds.DayOfWeek == date.DayOfWeek && ds.IsAvailable);

            var slots = new List<TimeSlotDTO>();
            foreach (var schedule in schedules)
            {
                var current = schedule.StartTime;
                while (current < schedule.EndTime)
                {
                    var end = current.Add(TimeSpan.FromMinutes(schedule.SlotDuration));
                    slots.Add(new TimeSlotDTO { StartTime = current, EndTime = end, IsAvailable = true });
                    current = end;
                }
            }

            return Result<IEnumerable<TimeSlotDTO>>.Ok(slots);
        }

        private DoctorDTO MapToDTO(Doctor doctor) => new DoctorDTO
        {
            Id = doctor.Id,
            FullName = doctor.FullName,
            Specialization = doctor.Specialization,
            LicenseNumber = doctor.LicenseNumber!,
            Phone = doctor.Phone,
            Email = doctor.Email,
            Bio = doctor.Bio,
            ConsultationFee = doctor.ConsultationFee
        };
    }
}