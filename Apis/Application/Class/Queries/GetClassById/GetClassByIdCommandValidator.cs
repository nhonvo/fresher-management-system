using Application.Class.Queries.GetClass;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class GetClassByIdCommandValidator : AbstractValidator<GetClassByIdQuery>
    {
        public GetClassByIdCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().NotNull();

        }
    }
}