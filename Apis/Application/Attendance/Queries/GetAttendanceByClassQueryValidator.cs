using Application.Account.Queries.GetAttendanceByClass;
using Application.Class.Queries.GetClass;
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