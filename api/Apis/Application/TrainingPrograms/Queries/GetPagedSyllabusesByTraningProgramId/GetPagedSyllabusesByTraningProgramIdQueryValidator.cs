using FluentValidation;

namespace Application.TrainingPrograms.Queries.GetPagedSyllabusesByTrainingProgramId;

public class GetPagedSyllabusesByTrainingProgramIdQueryValidator : AbstractValidator<GetPagedSyllabusesByTrainingProgramIdQuery>
{
    public GetPagedSyllabusesByTrainingProgramIdQueryValidator()
    {
        RuleFor(x => x.TrainingProgramId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(0);
    }
}
