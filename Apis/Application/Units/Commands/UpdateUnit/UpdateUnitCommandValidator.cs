using Application.Units.Commands.UpdateUnit;
using FluentValidation;

namespace Application.Units.Commands.CreateUnit
{
    public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
    {
        public UpdateUnitCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.SyllabusSession).GreaterThan(0);
            RuleFor(x => x.UnitNumber).GreaterThan(0);
            RuleFor(x => x.SyllabusId).GreaterThan(0);
        }
    }

}
