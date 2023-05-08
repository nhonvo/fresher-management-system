using FluentValidation;

namespace Application.Syllabuses.Commands
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