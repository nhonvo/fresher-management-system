using FluentValidation;

namespace Application.Class.Queries.GetClassDuration;

public class GetClassDurationQueryValidator : AbstractValidator<GetClassDurationQuery>
{
    public GetClassDurationQueryValidator()
    {
        RuleFor(x => x.id).NotEmpty().NotNull();
    }
}
