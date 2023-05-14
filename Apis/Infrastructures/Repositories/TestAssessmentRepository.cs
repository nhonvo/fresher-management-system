using Application.Interfaces;
using Application.Repositories;
using Application.ViewModels.TestAssessmentViewModels;
using Domain.Entities;
using Domain.Enums;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
#nullable disable warnings

namespace Infrastructures.Repositories
{
    public class TestAssessmentRepository : GenericRepository<TestAssessment>, ITestAssessmentRepository
    {
        private readonly ApplicationDbContext _context;

        public TestAssessmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<GetStudentTestScoreViewModel>> GetFinalScoreAsync(Expression<Func<TestAssessment, bool>> filter = null)
        {
            List<GetStudentTestScoreViewModel> result = await _context.TestAssessments.Where(filter).GroupBy(ta => new { ta.AttendeeId, ta.SyllabusId, ta.TrainingClassId, ta.TestAssessmentType })
                .Select(group => new GetStudentTestScoreViewModel
                {
                    AttendeeId = group.Key.AttendeeId,
                    SyllabusId = group.Key.SyllabusId,
                    TrainingClassId = group.Key.TrainingClassId,
                    TestAssessmentType = group.Key.TestAssessmentType.ToString(),
                    AverageScore = group.Average(ta => ta.Score),
                    SyllabusScheme = group.Key.TestAssessmentType == TestAssessmentType.Quiz ? _context.Syllabuses.FirstOrDefault(s => s.Id == group.Key.SyllabusId).QuizScheme
                    : group.Key.TestAssessmentType == TestAssessmentType.Assignment ? _context.Syllabuses.FirstOrDefault(s => s.Id == group.Key.SyllabusId).AssignmentScheme
                    : group.Key.TestAssessmentType == TestAssessmentType.FinalTheory ? _context.Syllabuses.Where(s => s.Id == group.Key.SyllabusId)
                                                                                                          .Select(x => new { FinalScheme = x.FinalTheoryScheme * x.FinalScheme })
                                                                                                          .FirstOrDefault().FinalScheme
                    : group.Key.TestAssessmentType == TestAssessmentType.FinalPractice ? _context.Syllabuses.Where(s => s.Id == group.Key.SyllabusId)
                                                                                                            .Select(x => new { FinalScheme = x.FinalPracticeScheme * x.FinalScheme })
                                                                                                            .FirstOrDefault().FinalScheme : 0
                }).OrderBy(x => x.TrainingClassId).ThenBy(x => x.SyllabusId)
                .ToListAsync();
            // paging

            return result;
        }
    }
}
