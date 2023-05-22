using Application.Units.Commands.UpdateUnit;
using FluentValidation;

namespace Application.Units.Commands.DeleteUnit
{
    public class DeleteUnitCommandValidator : AbstractValidator<DeleteUnitCommand>
    {
        public DeleteUnitCommandValidator()
        {
            RuleFor(x => x.id).GreaterThan(0);
        }
    }

}
