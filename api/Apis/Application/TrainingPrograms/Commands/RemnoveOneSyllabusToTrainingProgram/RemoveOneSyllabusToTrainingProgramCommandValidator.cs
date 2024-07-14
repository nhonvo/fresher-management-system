using FluentValidation;

namespace Application.TrainingPrograms.Commands.RemoveOneSyllabusToTrainingProgram
{
    public class RemoveOneSyllabusToTrainingProgramCommandValidator : AbstractValidator<RemoveOneSyllabusToTrainingProgramCommand>
    {
        public RemoveOneSyllabusToTrainingProgramCommandValidator()
        {
            RuleFor(x => x.syllabusId).NotEmpty().NotNull();
            RuleFor(x => x.trainingProgramId).NotEmpty().NotNull();
        }
    }
}