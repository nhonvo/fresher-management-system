using FluentValidation;

namespace Application.Class.Queries.GetClassById;

public class GetClassByIdQueryValidator : AbstractValidator<GetClassByIdQuery>
{
    public GetClassByIdQueryValidator()
    {
        RuleFor(x => x.id)
            .NotNull().NotEmpty()
            .GreaterThan(0);
    }
}
