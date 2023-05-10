using FluentValidation;

namespace Application.Attendances.Queries.GetAttendanceByClass
{
    public class GetAttendanceByClassQueryValidator : AbstractValidator<GetAttendanceByClassQuery>
    {
        public GetAttendanceByClassQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().NotNull();

        }
    }
}