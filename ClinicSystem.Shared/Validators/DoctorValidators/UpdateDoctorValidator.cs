using FluentValidation;
using ClinicSystem.Shared.DataTransferObject.DoctorDTOS;

namespace ClinicSystem.Shared.Validators.DoctorValidators
{
    public class UpdateDoctorValidator : AbstractValidator<AddDoctorDTO>
    {
        public UpdateDoctorValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("FullName is required")
                .MaximumLength(100);

            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Specialization is required")
                .MaximumLength(50);

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("LicenseNumber is required")
                .MaximumLength(20);

            RuleFor(x => x.ConsultationFee)
                .GreaterThan(0).WithMessage("ConsultationFee must be greater than 0")
                .LessThan(10000);

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .MaximumLength(100);

            RuleFor(x => x.Phone)
                .MaximumLength(15);
        }
    }
}