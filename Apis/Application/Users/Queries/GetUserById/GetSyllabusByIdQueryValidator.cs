using Application.Users.GetUserById.Queries;
using FluentValidation;

namespace Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().NotNull();
        }
    }
}