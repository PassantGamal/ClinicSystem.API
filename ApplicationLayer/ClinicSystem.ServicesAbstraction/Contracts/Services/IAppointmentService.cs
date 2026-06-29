using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.AppointmentDTOS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicSystem.ServicesAbstration.Contracts.Services
{
    public interface IAppointmentService
    {
        Task<Result<AppointmentDTO>> GetAppointmentByIdAsync(Guid id);
        Task<Result<IEnumerable<AppointmentDTO>>> GetAllAppointmentsAsync();
        Task<Result<IEnumerable<AppointmentDTO>>> GetDoctorAppointmentsAsync(Guid doctorId);
        Task<Result<IEnumerable<AppointmentDTO>>> GetPatientAppointmentsAsync(Guid patientId);
        Task<Result<AppointmentDTO>> BookAppointmentAsync(BookAppointmentDTO bookAppointmentDTO);
        Task<Result<AppointmentDTO>> UpdateAppointmentAsync(Guid id, BookAppointmentDTO bookAppointmentDTO);
        Task<Result> CancelAppointmentAsync(Guid id);
        Task<Result> CompleteAppointmentAsync(Guid id);
    }
}