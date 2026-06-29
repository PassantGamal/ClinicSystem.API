using FluentValidation;
using ClinicSystem.Shared.DataTransferObject.InvoiceDTOS;

namespace ClinicSystem.Shared.Validators.InvoiceValidators
{
    public class PayInvoiceValidator : AbstractValidator<PayInvoiceDTO>
    {
        public PayInvoiceValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("PaymentMethod is required")
                .MaximumLength(50);
        }
    }
}