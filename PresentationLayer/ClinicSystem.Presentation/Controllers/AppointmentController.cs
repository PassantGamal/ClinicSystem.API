using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.DataTransferObject.AppointmentDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.Presentation.Controllers
{
    [Authorize]
    public class AppointmentController : ApiBaseController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAllAppointments()
        {
            var result = await _appointmentService.GetAllAppointmentsAsync();
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointmentById(Guid id)
        {
            var result = await _appointmentService.GetAppointmentByIdAsync(id);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetDoctorAppointments(Guid doctorId)
        {
            var result = await _appointmentService.GetDoctorAppointmentsAsync(doctorId);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetPatientAppointments(Guid patientId)
        {
            var result = await _appointmentService.GetPatientAppointmentsAsync(patientId);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost]
        public async Task<ActionResult<AppointmentDTO>> BookAppointment(BookAppointmentDTO dto)
        {
            var result = await _appointmentService.BookAppointmentAsync(dto);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentDTO>> UpdateAppointment(Guid id, BookAppointmentDTO dto)
        {
            var result = await _appointmentService.UpdateAppointmentAsync(id, dto);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
        [HttpPut("{id}/cancel")]
        public async Task<ActionResult> CancelAppointment(Guid id)
        {
            var result = await _appointmentService.CancelAppointmentAsync(id);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPut("{id}/complete")]
        public async Task<ActionResult> CompleteAppointment(Guid id)
        {
            var result = await _appointmentService.CompleteAppointmentAsync(id);
            return HandleResult(result);
        }
    }
}