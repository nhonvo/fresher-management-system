using Application.Class.Commands.CreateClass;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {
         public CreateClassCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
         
        }
    }
}