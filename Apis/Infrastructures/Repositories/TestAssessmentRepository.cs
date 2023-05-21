using Application.Commons;
using Application.Repositories;
using Application.ViewModels.TestAssessmentViewModels;
using Domain.Entities;
using Domain.Enums;
using IdentityModel;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<Pagination<TestAssessment>> ToPagination(int pageIndex = 0, int pageSize = 10)
        {
            var itemCount = await _dbSet.Where(x => x.IsDeleted == false).CountAsync();
            var items = await _dbSet.Where(x => x.IsDeleted == false)
                                    .Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .Include(x => x.TrainingMaterials)
                                    .Include(x => x.Attendee)
                                    .AsNoTracking()
                                    .ToListAsync();

            var result = new Pagination<TestAssessment>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }

        public async Task<Pagination<TestAssessment>> GetAsync(
        Expression<Func<TestAssessment, bool>> filter = null,
           int pageIndex = 0,
           int pageSize = 10)
        {
            var query = _dbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var itemCount = await query.CountAsync();

            var items = await query.Include(x => x.TrainingMaterials)
                                   .Include(x => x.Attendee)
                                   .Skip(pageIndex * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            var result = new Pagination<TestAssessment>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
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
                    NumberOfTests = group.Count(),
                    SyllabusScheme = GetSyllabusAssessmentSchemeByType(group.Key.TestAssessmentType,
                                                                       _context.Syllabuses.FirstOrDefault(x => x.Id == group.Key.SyllabusId))
                }).OrderBy(x => x.TrainingClassId).ThenBy(x => x.SyllabusId)
                .ToListAsync();

            return result;
        }

        private static float GetSyllabusAssessmentSchemeByType(TestAssessmentType type, Syllabus syllabus)
        {
            float result = 0;
            switch (type)
            {
                case TestAssessmentType.Quiz:
                    result = syllabus.QuizScheme;
                    break;
                case TestAssessmentType.Assignment:
                    result = syllabus.AssignmentScheme;
                    break;
                case TestAssessmentType.FinalTheory:
                    result = syllabus.FinalTheoryScheme * syllabus.FinalScheme;
                    break;
                case TestAssessmentType.FinalPractice:
                    result = syllabus.FinalPracticeScheme * syllabus.FinalScheme;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
