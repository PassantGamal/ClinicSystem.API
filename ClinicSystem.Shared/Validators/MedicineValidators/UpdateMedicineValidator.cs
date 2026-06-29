using FluentValidation;
using ClinicSystem.Shared.DataTransferObject.MedicineDTOS;

namespace ClinicSystem.Shared.Validators.MedicineValidators
{
    public class UpdateMedicineValidator : AbstractValidator<AddMedicineDTO>
    {
        public UpdateMedicineValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.Category)
                .MaximumLength(50);

            RuleFor(x => x.Manufacturer)
                .MaximumLength(100);

            RuleFor(x => x.DosageForm)
                .MaximumLength(50);
        }
    }
}