using FluentValidation;

namespace Application.Lessons.Commands.UpdateUnitLesson
{
    public class UpdateUnitLessonCommandValidator : AbstractValidator<UpdateUnitLessonCommand>
    {
        public UpdateUnitLessonCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Duration).GreaterThan(0);
            RuleFor(x => x.LessonType).IsInEnum();
            RuleFor(x => x.DeliveryType).IsInEnum();
            RuleFor(x => x.UnitId).NotNull().NotEmpty();
        }
    }

}
