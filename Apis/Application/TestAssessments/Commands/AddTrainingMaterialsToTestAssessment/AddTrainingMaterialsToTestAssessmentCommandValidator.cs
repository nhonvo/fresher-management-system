using FluentValidation;

namespace Application.TestAssessments.Commands.AddTrainingMaterialsToTestAssessment;

public class AddTrainingMaterialsToTestAssessmentCommandValidator : AbstractValidator<AddTrainingMaterialsToTestAssessmentCommand>
{
    public AddTrainingMaterialsToTestAssessmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
        RuleFor(x => x.TrainingMaterials)
            .NotNull().NotEmpty();
    }
}
