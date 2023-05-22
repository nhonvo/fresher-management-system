using Application.Student.Commands.UpdateStudent;
using FluentValidation;

namespace Application.Lectures.Commands
{
    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0); ;
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Phone).NotEmpty().NotNull();
            RuleFor(x => x.DateOfBirth).NotNull().Must(BeValidDateOfBirth).WithMessage("The student must be at least 18 years old.");
        }
        private bool BeValidDateOfBirth(DateTime dob)
              => dob.AddYears(18) <= DateTime.Now;
    }
}