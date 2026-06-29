using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.DataTransferObject.AppointmentDTOS;
using ClinicSystem.Shared.DataTransferObject.MedicalHistoryDTOS;
using ClinicSystem.Shared.DataTransferObject.PatientDTOS;
using ClinicSystem.Shared.DataTransferObject.PrescriptionDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.Presentation.Controllers
{
    [Authorize]
    public class PatientController : ApiBaseController
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetAllPatients()
        {
            var result = await _patientService.GetAllPatientsAsync();
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetPatientById(Guid id)
        {
            var result = await _patientService.GetPatientByIdAsync(id);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor,Patient")]
        [HttpGet("{id}/medical-history")]
        public async Task<ActionResult<MedicalHistoryDTO>> GetPatientMedicalHistory(Guid id)
        {
            var result = await _patientService.GetPatientMedicalHistoryAsync(id);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor,Patient,Receptionist")]
        [HttpGet("{id}/appointments")]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetPatientAppointments(Guid id)
        {
            var result = await _patientService.GetPatientAppointmentsAsync(id);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor,Patient")]
        [HttpGet("{id}/prescriptions")]
        public async Task<ActionResult<IEnumerable<PrescriptionDTO>>> GetPatientPrescriptions(Guid id)
        {
            var result = await _patientService.GetPatientPrescriptionsAsync(id);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost]
        public async Task<ActionResult<PatientDTO>> CreatePatient(AddPatientDTO dto)
        {
            var result = await _patientService.CreatePatientAsync(dto);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDTO>> UpdatePatient(Guid id, AddPatientDTO dto)
        {
            var result = await _patientService.UpdatePatientAsync(id, dto);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(Guid id)
        {
            var result = await _patientService.DeletePatientAsync(id);
            return HandleResult(result);
        }
    }
}