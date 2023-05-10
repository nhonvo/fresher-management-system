using Application.Class.Commands.UpdateClass;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class UpdateClassCommandValidator : AbstractValidator<UpdateClassCommand>
    {
        public UpdateClassCommandValidator()
        {
            // RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);;
            // RuleFor(x => x.Name).NotEmpty().NotNull();
            // RuleFor(x => x.Code).NotEmpty().NotNull();
            // RuleFor(x => x.Location).NotNull();
            // RuleFor(x => x.AttendeeType).NotNull();
            // RuleFor(x => x.FSU).NotNull();
            // RuleFor(x => x.ClassTime).NotNull();
            // RuleFor(x => x.StartedOn).NotNull();
            // RuleFor(x => x.FinishedOn).NotNull();
            // RuleFor(x => x.Status).NotNull();
            // RuleFor(x => x.ApprovedOn).NotNull();
        }
    }
}