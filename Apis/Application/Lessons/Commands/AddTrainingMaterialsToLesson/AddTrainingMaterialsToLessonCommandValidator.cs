using FluentValidation;

namespace Application.Lessons.Commands.AddTrainingMaterialsToLesson;

public class AddTrainingMaterialsToLessonCommandValidator : AbstractValidator<AddTrainingMaterialsToLessonCommand>
{
    public AddTrainingMaterialsToLessonCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
        RuleFor(x => x.TrainingMaterials)
            .NotNull().NotEmpty();
    }
}
