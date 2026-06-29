using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.InvoiceDTOS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicSystem.ServicesAbstration.Contracts.Services
{
    public interface IInvoiceService
    {
        Task<Result<InvoiceDTO>> GetInvoiceByIdAsync(Guid id);
        Task<Result<IEnumerable<InvoiceDTO>>> GetAllInvoicesAsync();
        Task<Result<IEnumerable<InvoiceDTO>>> GetPatientInvoicesAsync(Guid patientId);
        Task<Result<InvoiceDTO>> CreateInvoiceAsync(CreateInvoiceDTO createInvoiceDTO);
        Task<Result<InvoiceDTO>> PayInvoiceAsync(Guid id, PayInvoiceDTO payInvoiceDTO);
        Task<Result> DeleteInvoiceAsync(Guid id);
    }
}