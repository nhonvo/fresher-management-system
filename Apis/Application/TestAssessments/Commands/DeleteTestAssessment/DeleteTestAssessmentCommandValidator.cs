using Application.TestAssessments.Commands.DeleteTestAssessment;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class DeleteTestAssessmentCommandValidator : AbstractValidator<DeleteTestAssessmentCommand>
    {
         public DeleteTestAssessmentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}