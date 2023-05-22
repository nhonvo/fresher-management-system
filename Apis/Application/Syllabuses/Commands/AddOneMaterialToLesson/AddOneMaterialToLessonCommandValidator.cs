using FluentValidation;

namespace Application.Syllabuses.Commands.AddOneMaterialToLesson
{
    public class AddOneMaterialToLessonCommandValidator : AbstractValidator<AddOneMaterialToLessonCommand>
    {
        public AddOneMaterialToLessonCommandValidator()
        {
            RuleFor(x => x.UnitLessonId).NotEmpty().NotNull();
        }
    }
}
// TODO: ADD MORE validation