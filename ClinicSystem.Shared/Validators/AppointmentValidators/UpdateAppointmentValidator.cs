using FluentValidation;
using ClinicSystem.Shared.DataTransferObject.AppointmentDTOS;

namespace ClinicSystem.Shared.Validators.AppointmentValidators
{
    public class UpdateAppointmentValidator : AbstractValidator<BookAppointmentDTO>
    {
        public UpdateAppointmentValidator()
        {
            RuleFor(x => x.DoctorEmail)
                .NotEmpty().WithMessage("DoctorEmail is required")
                .EmailAddress().WithMessage("DoctorEmail must be a valid email");

            RuleFor(x => x.PatientEmail)
                .NotEmpty().WithMessage("PatientEmail is required")
                .EmailAddress().WithMessage("PatientEmail must be a valid email");

            RuleFor(x => x.AppointmentDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("AppointmentDate must be in the future");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("StartTime is required");

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime).WithMessage("EndTime must be after StartTime");

            RuleFor(x => x.Reason)
                .MaximumLength(200);

            RuleFor(x => x.Notes)
                .MaximumLength(500);
        }
    }
}