using Application.Account.Queries.GetAttendanceByClass;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class GetAttendanceByClassQueryValidator : AbstractValidator<GetAttendanceByClassQuery>
    {
        public GetAttendanceByClassQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().NotNull();

        }
    }
}