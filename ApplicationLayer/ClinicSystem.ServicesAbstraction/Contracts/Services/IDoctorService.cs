using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.DoctorDTOS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicSystem.ServicesAbstration.Contracts.Services
{
    public interface IDoctorService
    {
        Task<Result<DoctorDTO>> GetDoctorByIdAsync(Guid id);
        Task<Result<IEnumerable<DoctorDTO>>> GetAllDoctorsAsync();
        Task<Result<IEnumerable<DoctorDTO>>> GetDoctorsBySpecializationAsync(string specialization);
        Task<Result<DoctorDTO>> CreateDoctorAsync(AddDoctorDTO addDoctorDTO);
        Task<Result<DoctorDTO>> UpdateDoctorAsync(Guid id, AddDoctorDTO addDoctorDTO);
        Task<Result> DeleteDoctorAsync(Guid id);
        Task<Result<IEnumerable<DoctorScheduleDTO>>> GetDoctorScheduleAsync(Guid doctorId);
        Task<Result<IEnumerable<TimeSlotDTO>>> GetAvailableSlotsAsync(Guid doctorId, DateTime date);
    }
}