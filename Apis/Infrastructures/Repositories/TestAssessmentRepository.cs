using Application.Interfaces;
using Application.Repositories;
using Application.ViewModels.TestAssessmentViewModels;
using Domain.Entities;
using Domain.Enums;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class TestAssessmentRepository : GenericRepository<TestAssessment>, ITestAssessmentRepository
    {
        private readonly ApplicationDbContext _context;

        public TestAssessmentRepository(ApplicationDbContext context, ICacheService cache) : base(context, cache)
        {
            _context = context;
        }

        public async Task<List<GetStudentTestScoreViewModel>> GetFinalScoreAsync(int id)
        {
            List<GetStudentTestScoreViewModel> result = await _context.TestAssessments.Where(x => x.AttendeeId == id).GroupBy(ta => new { ta.AttendeeId, ta.SyllabusId, ta.TrainingCLassId, ta.TestAssessmentType })
                .Select(group => new GetStudentTestScoreViewModel
                {
                    AttendeeId = group.Key.AttendeeId,
                    SyllabusId = group.Key.SyllabusId,
                    TrainingClassId = group.Key.TrainingCLassId,
                    TestAssessmentType = group.Key.TestAssessmentType,
                    AverageScore = group.Average(ta => ta.Score),
                    SyllabusScheme = group.Key.TestAssessmentType == TestAssessmentType.Quiz ? _context.Syllabuses.FirstOrDefault(s => s.Id == group.Key.SyllabusId).QuizScheme
                    : group.Key.TestAssessmentType == TestAssessmentType.Assignment ? _context.Syllabuses.FirstOrDefault(s => s.Id == group.Key.SyllabusId).AsignmentScheme
                    : group.Key.TestAssessmentType == TestAssessmentType.FinalTheory ? _context.Syllabuses.Where(s => s.Id == group.Key.SyllabusId).Select(x => new { FinalScheme = x.FinalTheoryScheme * x.FinalScheme}).FirstOrDefault().FinalScheme
                    : group.Key.TestAssessmentType == TestAssessmentType.FinalPractice ? _context.Syllabuses.Where(s => s.Id == group.Key.SyllabusId).Select(x => new { FinalScheme = x.FinalPraticeScheme * x.FinalScheme }).FirstOrDefault().FinalScheme : 0
                }).OrderBy(x => x.TrainingClassId).ThenBy(x => x.SyllabusId)
                .ToListAsync();
            return result;
        }
    }
}
