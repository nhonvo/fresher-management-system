using Application.ApproveRequests.Commands.CreateRequest;
using FluentValidation;

namespace Application.ApproveRequests.Commands
{
    public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestCommandValidator()
        {
            RuleFor(x => x.StudentId).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(x => x.ClassId).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}