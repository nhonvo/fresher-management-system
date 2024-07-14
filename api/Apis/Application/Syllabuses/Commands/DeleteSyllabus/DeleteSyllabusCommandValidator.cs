using Application.Syllabuses.Commands.DeleteSyllabus;
using FluentValidation;

namespace Application.Syllabuses.Queries.DeleteSyllabus
{
    public class DeleteSyllabusCommandValidator : AbstractValidator<DeleteSyllabusCommand>
    {
        public DeleteSyllabusCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().NotNull();
        }
    }
}