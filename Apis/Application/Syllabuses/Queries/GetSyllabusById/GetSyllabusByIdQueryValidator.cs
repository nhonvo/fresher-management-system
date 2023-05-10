using FluentValidation;

namespace Application.Syllabuses.Queries.GetSyllabusById
{
    public class GetSyllabusByIdQueryValidator : AbstractValidator<GetSyllabusByIdQuery>
    {
        public GetSyllabusByIdQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().NotNull();
        }
    }
}