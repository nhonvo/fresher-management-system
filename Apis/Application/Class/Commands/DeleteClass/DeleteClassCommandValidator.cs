using Application.Class.Commands.DeleteClass;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class DeleteClassCommandValidator : AbstractValidator<DeleteClassCommand>
    {
         public DeleteClassCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
         
        }
    }
}