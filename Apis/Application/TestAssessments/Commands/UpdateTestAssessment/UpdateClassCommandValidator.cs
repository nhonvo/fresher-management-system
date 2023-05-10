using Application.TestAssessments.Commands.UpdateTestAssessment;
using FluentValidation;

namespace Application.TestAssessments.Commands
{
    public class UpdateTestAssessmentCommandValidator : AbstractValidator<UpdateTestAssessmentCommand>
    {
        public UpdateTestAssessmentCommandValidator()
        {
            RuleFor(x => x.Score).GreaterThan(0);
            RuleFor(x => x.TestAssessmentType).NotNull();
            RuleFor(x => x.AttendeeId).GreaterThan(0);
            RuleFor(x => x.SyllabusId).GreaterThan(0);
        }
    }
}