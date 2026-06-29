using FluentValidation;
using ClinicSystem.Shared.DataTransferObject.PrescriptionDTOS;

namespace ClinicSystem.Shared.Validators.PrescriptionValidators
{
    public class AddPrescriptionMedicineValidator : AbstractValidator<AddPrescriptionMedicineDTO>
    {
        public AddPrescriptionMedicineValidator()
        {
            RuleFor(x => x.MedicineId).NotEmpty().WithMessage("MedicineId is required");
            RuleFor(x => x.Dosage).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Frequency).NotEmpty().MaximumLength(50);
            RuleFor(x => x.DurationDays).GreaterThan(0).WithMessage("DurationDays must be greater than 0");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}