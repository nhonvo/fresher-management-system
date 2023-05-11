using Application.ApproveRequests.Commands.AllowRequest;
using FluentValidation;

namespace Application.ApproveRequests.Commands
{
    public class AllowRequestCommandValidator : AbstractValidator<AllowRequestCommand>
    {
        public AllowRequestCommandValidator()
        {
            RuleFor(x => x.studentId).NotEmpty().NotNull();
            RuleFor(x => x.classId).NotEmpty().NotNull();
        }
    }
}