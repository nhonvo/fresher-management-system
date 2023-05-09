using Application.Syllabuses.Queries.ExportSyllabusesCSV;
using FluentValidation;

namespace Application.Syllabuses.Queries.GetSyllabusById
{
    public class ExportSyllabusesCSVQueryValidator : AbstractValidator<ExportSyllabusesCSVQuery>
    {
        public ExportSyllabusesCSVQueryValidator()
        {
            RuleFor(x => x.columnSeparator).NotEmpty().NotNull();
        }
    }
}