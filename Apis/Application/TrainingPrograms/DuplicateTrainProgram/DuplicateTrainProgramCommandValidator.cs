using FluentValidation;

namespace Application.TrainingPrograms.Commands.DuplicateTrainProgram
{
    public class DuplicateTrainProgramCommandValidator : AbstractValidator<DuplicateTrainProgramCommand>
    {
        public DuplicateTrainProgramCommandValidator()
        {
            RuleFor(c => c.id).NotEmpty();
        }
    }
}