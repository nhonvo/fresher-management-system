using Application.ApproveRequests.Commands.AllowRequest;
using Application.ApproveRequests.Commands.CreateRequest;
using Application.Student.Commands.AddStudent;
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