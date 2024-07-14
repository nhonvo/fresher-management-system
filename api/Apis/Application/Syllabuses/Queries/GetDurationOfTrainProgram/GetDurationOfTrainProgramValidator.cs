using Application.Syllabuses.Queries.GetDurationOfTrainProgram;
using FluentValidation;

namespace Application.Syllabuses.Queries.GetSyllabusById
{
    public class GetDurationOfTrainProgramValidator : AbstractValidator<GetDurationOfTrainProgramQuery>
    {
        public GetDurationOfTrainProgramValidator()
        {
            RuleFor(x => x.id).NotEmpty().NotNull();
        }
    }
}