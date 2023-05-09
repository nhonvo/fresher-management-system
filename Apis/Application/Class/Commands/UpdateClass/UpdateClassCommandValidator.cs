using Application.Class.Commands.UpdateClass;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class UpdateClassCommandValidator : AbstractValidator<UpdateClassCommand>
    {
         public UpdateClassCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
         
        }
    }
}