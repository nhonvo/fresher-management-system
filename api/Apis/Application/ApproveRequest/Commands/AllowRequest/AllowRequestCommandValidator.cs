using Application.ApproveRequests.Commands.AllowRequest;
using FluentValidation;

namespace Application.ApproveRequests.Commands
{
    public class AllowRequestCommandValidator : AbstractValidator<AllowRequestCommand>
    {
        public AllowRequestCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().NotNull().GreaterThan(0);
            // RuleFor(x => x.allowJoin)
            //     .NotEmpty().NotNull()
            //     .InclusiveBetween(false, true)
            //     .WithMessage("'allowJoin' must be either true or false.");
        }
    }
}