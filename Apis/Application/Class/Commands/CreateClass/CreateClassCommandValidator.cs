using Application.Class.Commands.CreateClass;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {
        public CreateClassCommandValidator()
        {
            RuleFor(c => c.ClassName).NotEmpty();
            RuleFor(c => c.ClassTimeStart).NotEmpty().LessThan(c => c.ClassTimeEnd);
            RuleFor(c => c.ClassTimeEnd).NotEmpty().GreaterThan(c => c.ClassTimeStart);
            RuleFor(c => c.NumberAttendeePlanned).NotEmpty().GreaterThan(0);
            RuleFor(c => c.NumberAttendeeAccepted).GreaterThanOrEqualTo(0);
            RuleFor(c => c.NumberAttendeeActual).GreaterThanOrEqualTo(0);
            RuleFor(c => c.ClassLocation).NotNull();
            RuleFor(c => c.Status).IsInEnum();
        }
    }
}