using FluentValidation;

namespace Application.Syllabuses.Commands.CreateSyllabus
{
    public class CreateSyllabusCommandValidator : AbstractValidator<CreateSyllabusCommand>
    {
        public CreateSyllabusCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull();
            // add more validation
        }
    }
}