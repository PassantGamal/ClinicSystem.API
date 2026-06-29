using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.DataTransferObject.PrescriptionDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Doctor")]
    public class PrescriptionController : ApiBaseController
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPrescriptionPdfService _pdfService;

        public PrescriptionController(IPrescriptionService prescriptionService, IPrescriptionPdfService pdfService)
        {
            _prescriptionService = prescriptionService;
            _pdfService = pdfService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionDTO>>> GetAllPrescriptions()
        {
            var result = await _prescriptionService.GetAllPrescriptionsAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionDTO>> GetPrescriptionById(Guid id)
        {
            var result = await _prescriptionService.GetPrescriptionByIdAsync(id);
            return HandleResult(result);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<PrescriptionDTO>>> GetDoctorPrescriptions(Guid doctorId)
        {
            var result = await _prescriptionService.GetDoctorPrescriptionsAsync(doctorId);
            return HandleResult(result);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<PrescriptionDTO>>> GetPatientPrescriptions(Guid patientId)
        {
            var result = await _prescriptionService.GetPatientPrescriptionsAsync(patientId);
            return HandleResult(result);
        }

        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> GetPrescriptionPdf(Guid id)
        {
            var result = await _prescriptionService.GetPrescriptionByIdAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Errors.FirstOrDefault()?.Description ?? "Prescription not found");

            var pdfBytes = await _pdfService.GeneratePrescriptionPdfAsync(result.Value);
            return File(pdfBytes, "application/pdf", $"Prescription_{id}.pdf");
        }

        [HttpPost]
        public async Task<ActionResult<PrescriptionDTO>> CreatePrescription(CreatePrescriptionDTO dto)
        {
            var result = await _prescriptionService.CreatePrescriptionAsync(dto);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PrescriptionDTO>> UpdatePrescription(Guid id, CreatePrescriptionDTO dto)
        {
            var result = await _prescriptionService.UpdatePrescriptionAsync(id, dto);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrescription(Guid id)
        {
            var result = await _prescriptionService.DeletePrescriptionAsync(id);
            return HandleResult(result);
        }
    }
}