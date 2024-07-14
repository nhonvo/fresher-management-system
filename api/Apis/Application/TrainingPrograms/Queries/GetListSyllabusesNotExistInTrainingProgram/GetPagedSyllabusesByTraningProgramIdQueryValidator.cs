using FluentValidation;

namespace Application.TrainingPrograms.Queries.GetListSyllabusesNotExistInTrainingProgram;

public class GetListSyllabusesNotExistInTrainingProgramQueryValidator : AbstractValidator<GetListSyllabusesNotExistInTrainingProgramQuery>
{
    public GetListSyllabusesNotExistInTrainingProgramQueryValidator()
    {
        RuleFor(x => x.TrainingProgramId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(0);
    }
}
