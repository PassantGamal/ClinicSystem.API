using FluentValidation;
using ClinicSystem.Shared.DataTransferObject.PatientDTOS;

namespace ClinicSystem.Shared.Validators.PatientValidators
{
    public class UpdatePatientValidator : AbstractValidator<AddPatientDTO>
    {
        public UpdatePatientValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("FullName is required")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .MaximumLength(100);

            RuleFor(x => x.Phone)
                .MaximumLength(15);

            RuleFor(x => x.Gender)
                .MaximumLength(10);

            RuleFor(x => x.BloodType)
                .MaximumLength(5);

            RuleFor(x => x.Address)
                .MaximumLength(200);

            RuleFor(x => x.EmergencyContact)
                .MaximumLength(15);
        }
    }
}