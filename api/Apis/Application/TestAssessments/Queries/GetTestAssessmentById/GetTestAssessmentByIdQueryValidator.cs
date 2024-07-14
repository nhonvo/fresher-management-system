using Application.TestAssessments.Queries.GetTestAssessmentById;
using FluentValidation;

namespace Application.Syllabuses.Queries.GetTestAssesmentById;

public class GetTestAssessmentByIdQueryValidator : AbstractValidator<GetTestAssessmentByIdQuery>
{
    public GetTestAssessmentByIdQueryValidator()
    {
        RuleFor(x => x.id).NotEmpty().NotNull();
    }
}
