using FluentValidation;

namespace Application.Lectures.Commands
{
    public class CreateLectureCommandValidator : AbstractValidator<CreateLectureCommand>
    {
         public CreateLectureCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x=>x.Duration).NotEmpty().NotNull();
            RuleFor(x=>x.LessonType).NotEmpty().NotNull();
            RuleFor(x=>x.OutputStandardId).NotEmpty().NotNull();
            // add more validation
        }
    }
}