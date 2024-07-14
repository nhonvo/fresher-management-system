using Application.Common.Exceptions;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TestAssessments.Queries.CalculatorAverageOfStudentInSyllabus
{
    public record CalculatorAverageOfStudentInSyllabusQuery(int trainingClassId, int syllabusId, int attendeeId) : IRequest<float>;

    public class CalculatorAverageOfStudentInSyllabusHandler : IRequestHandler<CalculatorAverageOfStudentInSyllabusQuery, float>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalculatorAverageOfStudentInSyllabusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<float> Handle(CalculatorAverageOfStudentInSyllabusQuery request, CancellationToken cancellationToken)
        {
            var studentAssignment = (await _unitOfWork.TestAssessmentRepository.GetAsync(
                filter: x => x.AttendeeId == request.attendeeId
                             && x.SyllabusId == request.syllabusId
                             && x.TrainingClassId == request.trainingClassId,
                include: x => x.Include(x => x.Syllabus),
                pageIndex: 0,
                pageSize: int.MaxValue)).Items;
            if (studentAssignment is null)
            {
                throw new NotFoundException("Student assignment not found");
            }
            var getSyllabus = studentAssignment.Select(x => x.Syllabus).FirstOrDefault();
            var ratio = new
            {
                QuizSchema = getSyllabus.QuizScheme,
                AssignmentScheme = getSyllabus.AssignmentScheme,
                FinalScheme = getSyllabus.FinalScheme,
                FinalTheoryScheme = getSyllabus.FinalTheoryScheme,
                FinalPractice = getSyllabus.FinalPracticeScheme
            };

            var assignmentGroups = studentAssignment.GroupBy(x => x.TestAssessmentType);

            var averageQuiz = assignmentGroups.FirstOrDefault(x => x.Key == TestAssessmentType.Quiz)?.Average(x => x.Score) ?? 0;
            var averageAssignment = assignmentGroups.FirstOrDefault(x => x.Key == TestAssessmentType.Assignment)?.Average(x => x.Score) ?? 0;
            var averageFinalTheory = assignmentGroups.FirstOrDefault(x => x.Key == TestAssessmentType.FinalTheory)?.Average(x => x.Score) ?? 0;
            var averageFinalPractice = assignmentGroups.FirstOrDefault(x => x.Key == TestAssessmentType.FinalPractice)?.Average(x => x.Score) ?? 0;

            var averageFinal = (averageQuiz / 100 * ratio.QuizSchema)
                               + (averageAssignment / 100 * ratio.AssignmentScheme)
                               + ((averageFinalTheory / 100 * ratio.FinalTheoryScheme)
                                  + (averageFinalPractice / 100 * ratio.FinalPractice))
                               * ratio.FinalScheme / 100;
            return averageFinal;
        }
    }
}
