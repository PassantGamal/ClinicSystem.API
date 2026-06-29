using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Enums;
using ClinicSystem.ServicesAbstration.Contracts.Repositories;
using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.InvoiceDTOS;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Services.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<InvoiceDTO>> GetInvoiceByIdAsync(Guid id)
        {
            var invoices = await _unitOfWork.Repository<Invoice>()
                .FindAsync(i => i.Id == id,
                    q => q.Include(i => i.Patient));

            var invoice = invoices.FirstOrDefault();
            if (invoice is null)
                return Result<InvoiceDTO>.Fail(Error.NotFound("Invoice.NotFound", "Invoice not found"));

            return Result<InvoiceDTO>.Ok(MapToDTO(invoice));
        }

        public async Task<Result<IEnumerable<InvoiceDTO>>> GetAllInvoicesAsync()
        {
            var invoices = await _unitOfWork.Repository<Invoice>()
                .FindAsync(i => true,
                    q => q.Include(i => i.Patient));

            return Result<IEnumerable<InvoiceDTO>>.Ok(invoices.Select(MapToDTO));
        }

        public async Task<Result<IEnumerable<InvoiceDTO>>> GetPatientInvoicesAsync(Guid patientId)
        {
            var invoices = await _unitOfWork.Repository<Invoice>()
                .FindAsync(i => i.PatientId == patientId,
                    q => q.Include(i => i.Patient));

            return Result<IEnumerable<InvoiceDTO>>.Ok(invoices.Select(MapToDTO));
        }

        public async Task<Result<InvoiceDTO>> CreateInvoiceAsync(CreateInvoiceDTO dto)
        {
            var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(dto.AppointmentId);
            if (appointment is null)
                return Result<InvoiceDTO>.Fail(Error.NotFound("Appointment.NotFound", "Appointment not found"));

            var patient = await _unitOfWork.Repository<Patient>().GetByIdAsync(dto.PatientId);
            if (patient is null)
                return Result<InvoiceDTO>.Fail(Error.NotFound("Patient.NotFound", "Patient not found"));

            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                AppointmentId = dto.AppointmentId,
                PatientId = dto.PatientId,
                Amount = dto.Amount,
                PaidAmount = 0,
                PaymentStatus = PaymentStatus.Pending,
                InvoiceDate = DateTime.UtcNow,
                PaymentMethod = dto.PaymentMethod
            };

            await _unitOfWork.Repository<Invoice>().AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync();

            var createdInvoice = await _unitOfWork.Repository<Invoice>()
                .FindAsync(i => i.Id == invoice.Id,
                    q => q.Include(i => i.Patient));

            return Result<InvoiceDTO>.Ok(MapToDTO(createdInvoice.FirstOrDefault()!));
        }

        public async Task<Result<InvoiceDTO>> PayInvoiceAsync(Guid id, PayInvoiceDTO dto)
        {
            var invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(id);
            if (invoice is null)
                return Result<InvoiceDTO>.Fail(Error.NotFound("Invoice.NotFound", "Invoice not found"));

            if (invoice.PaymentStatus == PaymentStatus.Paid)
                return Result<InvoiceDTO>.Fail(Error.Validation("Invoice.AlreadyPaid", "Invoice is already paid"));

            invoice.PaidAmount += dto.Amount;
            invoice.PaymentMethod = dto.PaymentMethod;
            invoice.PaymentDate = DateTime.UtcNow;
            invoice.PaymentStatus = invoice.PaidAmount >= invoice.Amount ? PaymentStatus.Paid : PaymentStatus.Partial;
            invoice.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Invoice>().Update(invoice);
            await _unitOfWork.SaveChangesAsync();

            var updatedInvoice = await _unitOfWork.Repository<Invoice>()
                .FindAsync(i => i.Id == id,
                    q => q.Include(i => i.Patient));

            return Result<InvoiceDTO>.Ok(MapToDTO(updatedInvoice.FirstOrDefault()!));
        }

        public async Task<Result> DeleteInvoiceAsync(Guid id)
        {
            var invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(id);
            if (invoice is null)
                return Result.Fail(Error.NotFound("Invoice.NotFound", "Invoice not found"));

            invoice.IsDeleted = true;
            invoice.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Invoice>().Update(invoice);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        private InvoiceDTO MapToDTO(Invoice invoice)
        {
            return new InvoiceDTO
            {
                Id = invoice.Id,
                AppointmentId = invoice.AppointmentId,
                PatientId = invoice.PatientId,
                PatientName = invoice.Patient?.FullName ?? "Unknown",
                Amount = invoice.Amount,
                PaidAmount = invoice.PaidAmount,
                PaymentStatus = invoice.PaymentStatus.ToString(),
                InvoiceDate = invoice.InvoiceDate,
                PaymentDate = invoice.PaymentDate,
                PaymentMethod = invoice.PaymentMethod
            };
        }
    }
}