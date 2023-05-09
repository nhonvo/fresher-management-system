using FluentValidation;

namespace Application.Syllabuses.Queries.GetPagedSyllabusesByDateRange;

public class GetPagedSyllabusesByDateRangeQueryValidator : AbstractValidator<GetPagedSyllabusesByDateRangeQuery>
{
    public GetPagedSyllabusesByDateRangeQueryValidator()
    {
        RuleFor(x => x.FromDate).NotNull().NotEqual(DateTime.MinValue);
        RuleFor(x => x.ToDate).NotNull().NotEqual(DateTime.MinValue);
        RuleFor(x => x.FromDate).LessThanOrEqualTo(x => x.ToDate);
        RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(0);
    }
}
