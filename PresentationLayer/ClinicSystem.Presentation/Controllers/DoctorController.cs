using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.DataTransferObject.DoctorDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.Presentation.Controllers
{
    [Authorize]
    public class DoctorController : ApiBaseController
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetAllDoctors()
        {
            var result = await _doctorService.GetAllDoctorsAsync();
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDTO>> GetDoctorById(Guid id)
        {
            var result = await _doctorService.GetDoctorByIdAsync(id);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
        [HttpGet("specialization/{specialization}")]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetDoctorsBySpecialization(string specialization)
        {
            var result = await _doctorService.GetDoctorsBySpecializationAsync(specialization);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [HttpGet("{id}/schedule")]
        public async Task<ActionResult<IEnumerable<DoctorScheduleDTO>>> GetDoctorSchedule(Guid id)
        {
            var result = await _doctorService.GetDoctorScheduleAsync(id);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
        [HttpGet("{id}/slots")]
        public async Task<ActionResult<IEnumerable<TimeSlotDTO>>> GetAvailableSlots(Guid id, [FromQuery] DateTime date)
        {
            var result = await _doctorService.GetAvailableSlotsAsync(id, date);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<DoctorDTO>> CreateDoctor(AddDoctorDTO dto)
        {
            var result = await _doctorService.CreateDoctorAsync(dto);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorDTO>> UpdateDoctor(Guid id, AddDoctorDTO dto)
        {
            var result = await _doctorService.UpdateDoctorAsync(id, dto);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(Guid id)
        {
            var result = await _doctorService.DeleteDoctorAsync(id);
            return HandleResult(result);
        }
    }
}