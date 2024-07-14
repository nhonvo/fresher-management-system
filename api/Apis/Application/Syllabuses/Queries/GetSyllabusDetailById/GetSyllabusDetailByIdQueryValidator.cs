using FluentValidation;

namespace Application.Syllabuses.Queries.GetSyllabusDetailById
{
    public class GetSyllabusDetailByIdQueryValidator : AbstractValidator<GetSyllabusDetailByIdQuery>
    {
        public GetSyllabusDetailByIdQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().NotNull();
        }
    }
}