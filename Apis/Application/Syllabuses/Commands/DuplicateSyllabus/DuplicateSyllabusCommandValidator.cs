using Application.Syllabuses.Commands.DeleteSyllabus;
using Application.Syllabuses.Commands.DuplicateSyllabus;
using FluentValidation;

namespace Application.Syllabuses.Queries.DeleteSyllabus
{
    public class DuplicateSyllabusCommandValidator : AbstractValidator<DuplicateSyllabusCommand>
    {
        public DuplicateSyllabusCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().NotNull();
        }
    }
}