using FluentValidation;

namespace Application.TestAssessments.Commands.CreateTestAssessment
{
    public class CreateTestAssessmentCommandValidator : AbstractValidator<CreateTestAssessmentCommand>
    {
        public CreateTestAssessmentCommandValidator()
        {
            RuleFor(x => x.Score).GreaterThan(0);
            RuleFor(x => x.TestAssessmentType).NotNull();
            RuleFor(x => x.AttendeeId).GreaterThan(0);
            RuleFor(x => x.SyllabusId).GreaterThan(0);
        }
    }
}