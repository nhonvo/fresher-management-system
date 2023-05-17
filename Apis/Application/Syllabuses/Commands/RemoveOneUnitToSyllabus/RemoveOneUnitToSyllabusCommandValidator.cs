using FluentValidation;

namespace Application.Syllabuses.Commands.RemoveOneUnitToSyllabus
{
    public class RemoveOneUnitToSyllabusCommandValidator : AbstractValidator<RemoveOneUnitToSyllabusCommand>
    {
        public RemoveOneUnitToSyllabusCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull();
            // add more validation
        }
    }
}