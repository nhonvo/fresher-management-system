using Application.TestAssessments.Queries.CalculatorAverageOfStudentInSyllabus;
using Application.TestAssessments.Queries.GetTestAssessmentById;
using FluentValidation;

namespace Application.Syllabuses.Queries.CalculatorAverageOfStudentInSyllabus;

public class CalculatorAverageOfStudentInSyllabusValidator : AbstractValidator<CalculatorAverageOfStudentInSyllabusQuery>
{
    public CalculatorAverageOfStudentInSyllabusValidator()
    {
        RuleFor(x => x.trainingClassId).NotEmpty().NotNull();
        RuleFor(x => x.syllabusId).NotEmpty().NotNull();
        RuleFor(x => x.attendeeId).NotEmpty().NotNull();
    }
}
