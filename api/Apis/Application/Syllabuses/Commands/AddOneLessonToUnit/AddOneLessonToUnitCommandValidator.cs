using FluentValidation;

namespace Application.Syllabuses.Commands.AddOneLessonToUnit
{
    public class AddOneLessonToUnitCommandValidator : AbstractValidator<AddOneLessonToUnitCommand>
    {
        public AddOneLessonToUnitCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.UnitId).NotEmpty().NotNull();
        }
    }
}// TODO: add validation 