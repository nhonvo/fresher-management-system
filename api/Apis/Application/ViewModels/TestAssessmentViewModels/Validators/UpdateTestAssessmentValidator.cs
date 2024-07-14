using FluentValidation;

namespace Application.ViewModels.TestAssessmentViewModels.Validators
{
    public class UpdateTestAssessmentValidator : AbstractValidator<UpdateTestAssessmentViewModel>
    {
        public UpdateTestAssessmentValidator()
        {
            RuleFor(x => x.AttendeeId)
                .NotEmpty().WithMessage("Attendee Id is required.");
            RuleFor(x => x.SyllabusId)
                .NotEmpty().WithMessage("Syllabus Id is required.");
            RuleFor(x => x.TrainingClassId)
                .NotEmpty().WithMessage("Training Class Id is required.");
            RuleFor(x => x.TestAssessmentType)
                .NotEmpty().WithMessage("TestAssessment Type Id is required.");
            RuleFor(x => x.Score)
                .LessThanOrEqualTo(10).WithMessage("Score must less than or equal to 10")
                .GreaterThanOrEqualTo(0).WithMessage("Score must grater than or equal to 0");
        }
    }
}
