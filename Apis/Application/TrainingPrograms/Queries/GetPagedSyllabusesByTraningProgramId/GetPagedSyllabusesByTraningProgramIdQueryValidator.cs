using FluentValidation;

namespace Application.TrainingPrograms.Queries.GetPagedSyllabusesByTraningProgramId;

public class GetPagedSyllabusesByTraningProgramIdQueryValidator : AbstractValidator<GetPagedSyllabusesByTraningProgramIdQuery>
{
    public GetPagedSyllabusesByTraningProgramIdQueryValidator()
    {
        RuleFor(x => x.TrainingProgramId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(0);
    }
}
