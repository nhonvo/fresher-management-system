using FluentValidation;

namespace Application.TrainingPrograms.Commands.AddOneSyllabusToTrainingProgram
{
    public class AddOneSyllabusToTrainingProgramCommandValidator : AbstractValidator<AddOneSyllabusToTrainingProgramCommand>
    {
        public AddOneSyllabusToTrainingProgramCommandValidator()
        {
            RuleFor(x => x.syllabusId).NotEmpty().NotNull();
            RuleFor(x => x.trainingProgramId).NotEmpty().NotNull();
        }
    }
}