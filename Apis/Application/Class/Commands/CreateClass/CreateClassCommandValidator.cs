using Application.Class.Commands.CreateClass;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {
        public CreateClassCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Code).NotEmpty().NotNull();
            RuleFor(x => x.Location).NotNull();
            RuleFor(x => x.AttendeeType).NotNull();
            RuleFor(x => x.FSU).NotNull();
            RuleFor(x => x.ClassTime).NotNull();
            RuleFor(x => x.StartedOn).NotNull();
            RuleFor(x => x.FinishedOn).NotNull();
            RuleFor(x => x.Status).NotNull();
            RuleFor(x => x.ApprovedOn).NotNull();
        }
    }
}