using Application.ApproveRequests.Commands.CreateRequest;
using FluentValidation;

namespace Application.ApproveRequests.Commands.CreateRequestCurrentUser
{
    public class CreateRequestCurrentUserValidator : AbstractValidator<CreateRequestCurrentUserCommand>
    {
        public CreateRequestCurrentUserValidator()
        {
            RuleFor(x => x.ClassId).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}