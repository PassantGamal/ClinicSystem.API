using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.DataTransferObject.InvoiceDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.Presentation.Controllers
{
    [Authorize]
    public class InvoiceController : ApiBaseController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetAllInvoices()
        {
            var result = await _invoiceService.GetAllInvoicesAsync();
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Patient")]
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDTO>> GetInvoiceById(Guid id)
        {
            var result = await _invoiceService.GetInvoiceByIdAsync(id);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Patient")]
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetPatientInvoices(Guid patientId)
        {
            var result = await _invoiceService.GetPatientInvoicesAsync(patientId);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost]
        public async Task<ActionResult<InvoiceDTO>> CreateInvoice(CreateInvoiceDTO dto)
        {
            var result = await _invoiceService.CreateInvoiceAsync(dto);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Patient")]
        [HttpPut("{id}/pay")]
        public async Task<ActionResult<InvoiceDTO>> PayInvoice(Guid id, PayInvoiceDTO dto)
        {
            var result = await _invoiceService.PayInvoiceAsync(id, dto);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInvoice(Guid id)
        {
            var result = await _invoiceService.DeleteInvoiceAsync(id);
            return HandleResult(result);
        }
    }
}