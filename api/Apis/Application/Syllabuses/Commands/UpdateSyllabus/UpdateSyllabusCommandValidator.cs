using FluentValidation;

namespace Application.Syllabuses.Commands.UpdateSyllabus
{
    public class UpdateSyllabusCommandValidator : AbstractValidator<UpdateSyllabusCommand>
    {
        public UpdateSyllabusCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull();
            // add more validation
        }
    }
}