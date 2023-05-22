using FluentValidation;

namespace Application.Lessons.Commands.CreateUnitLesson
{
    public class CreateUnitLessonCommandValidator : AbstractValidator<CreateUnitLessonCommand>
    {
        public CreateUnitLessonCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Duration).GreaterThan(0);
            RuleFor(x => x.LessonType).IsInEnum();
            RuleFor(x => x.DeliveryType).IsInEnum();
        }
    }

}
