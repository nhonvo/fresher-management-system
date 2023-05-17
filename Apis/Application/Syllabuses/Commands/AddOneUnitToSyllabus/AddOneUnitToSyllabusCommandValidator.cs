using FluentValidation;

namespace Application.Syllabuses.Commands.AddOneUnitToSyllabus
{
    public class AddOneUnitToSyllabusCommandValidator : AbstractValidator<AddOneUnitToSyllabusCommand>
    {
        public AddOneUnitToSyllabusCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.SyllabusSession).NotEmpty().NotNull();
            RuleFor(x => x.UnitNumber).NotEmpty().NotNull();
            RuleFor(x => x.SyllabusId).NotEmpty().NotNull();
        }
    }
}