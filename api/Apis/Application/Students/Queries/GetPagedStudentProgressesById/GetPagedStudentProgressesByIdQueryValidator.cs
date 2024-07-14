using Application.StudentProgresses.Queries.GetPagedStudentProgressesById;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class GetPagedStudentProgressesByIdQueryValidator : AbstractValidator<GetPagedStudentProgressesByIdQuery>
    {
        public GetPagedStudentProgressesByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0); ;
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}