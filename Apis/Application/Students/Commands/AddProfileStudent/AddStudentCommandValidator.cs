using Application.Student.Commands.AddStudent;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class AddStudentCommandValidator : AbstractValidator<AddStudentCommand>
    {
        public AddStudentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0); ;
        }
    }
}