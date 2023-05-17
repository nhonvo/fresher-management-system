using FluentValidation;

namespace Application.Syllabuses.Commands.AddOneUnitToSyllabus
{
    public class AddOneUnitToSyllabusCommandValidator : AbstractValidator<AddOneUnitToSyllabusCommand>
    {
        public AddOneUnitToSyllabusCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull();
            // add more validation
        }
    }
}