using Application.Class.Commands.UpdateClass;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class UpdateClassCommandValidator : AbstractValidator<UpdateClassCommand>
    {
        public UpdateClassCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0); ;
            RuleFor(c => c.ClassName).NotEmpty();
            RuleFor(c => c.ClassCode).NotEmpty();
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