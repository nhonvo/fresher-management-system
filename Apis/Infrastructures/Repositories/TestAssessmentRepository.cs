using Application.Commons;
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
            List<GetStudentTestScoreViewModel> result = await _context.TestAssessments.Where(filter).Include(x => x.TrainingMaterials).GroupBy(ta => new { ta.AttendeeId, ta.SyllabusId, ta.TrainingClassId, ta.TestAssessmentType })
                .Select(group => new GetStudentTestScoreViewModel
                {
                    AttendeeId = group.Key.AttendeeId,
                    SyllabusId = group.Key.SyllabusId,
                    TrainingClassId = group.Key.TrainingClassId,
                    TestAssessmentType = group.Key.TestAssessmentType.ToString(),
                    AverageScore = (float)Math.Round((float)group.Average(ta => ta.Score), 2),
                    NumberOfTests = group.Count(),
                    SyllabusScheme = GetSyllabusAssessmentSchemeByType(group.Key.TestAssessmentType,
                                                                       _context.Syllabuses.FirstOrDefault(x => x.Id == group.Key.SyllabusId)),
                    AssessmentList = group.Select(ta => new GetStudentTestScore_TestAssessmentViewModel
                    {
                        Id = ta.Id,
                        Score = ta.Score,
                        TrainingMaterials = ta.TrainingMaterials.Select(tm => new GetStudentTestScore_TestAssessment_TrainingMaterialViewModel
                        {
                            Id = tm.Id,
                            FileName = tm.FileName,
                            FilePath = tm.FilePath,
                            FileSize = tm.FileSize
                        }).ToList()
                    }).ToList()
                }).OrderBy(x => x.TrainingClassId).ThenBy(x => x.SyllabusId)
                .ToListAsync();

            return result;
        }
        // private static float GetSyllabusAssessmentSchemeByType(TestAssessmentType type, Syllabus syllabus)
        // {
        //     return type switch
        //     {
        //         TestAssessmentType.Quiz => syllabus.QuizScheme,
        //         TestAssessmentType.Assignment => syllabus.AssignmentScheme,
        //         TestAssessmentType.FinalTheory => syllabus.FinalTheoryScheme * syllabus.FinalScheme,
        //         TestAssessmentType.FinalPractice => syllabus.FinalPracticeScheme * syllabus.FinalScheme,
        //         _ => throw new Transaction...() // Handle the default case accordingly
        //     };
        // }
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
