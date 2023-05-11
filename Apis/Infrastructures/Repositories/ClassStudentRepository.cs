using Application.Commons;
using Application.Interfaces;
using Application.Repositories;
using Application.Students.DTO;
using Domain.Entities;
using Domain.Enums;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories;

public class ClassStudentRepository : GenericRepository<ClassStudent>, IClassStudentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ClassStudentRepository(ApplicationDbContext context, ICacheService cache) : base(context, cache)
    {
        _dbContext = context;
    }

    public async Task<Pagination<StudentProgressDTO>> GetPagedStudentProgressesById(
        int id,
        int pageIndex = 0,
        int pageSize = 10)
    {
        var query = _dbSet.AsQueryable().AsNoTracking();

        // filter
        query = query.Where(x => x.UserId == id);

        // include
        query = query
            .Include(x => x.TrainingClass)
                .ThenInclude(x => x.TrainingProgram)
                .ThenInclude(x => x.ProgramSyllabus)
                .ThenInclude(x => x.Syllabus);

        // paging
        query = query
            .Skip(pageIndex * pageSize)
            .Take(pageSize);

        var count = await query.CountAsync();
        var classStudentList = await query.ToListAsync();

        var studentProgressList = new List<StudentProgressDTO>();

        foreach (var classStudent in classStudentList)
        {
            if (classStudent.TrainingClass?.TrainingProgram == null) continue;
            var syllabusGPAList = new List<float>();
            var studentClassQuizScoreList = new List<float>();
            var studentClassAssignmentScoreList = new List<float>();
            var studentClassFinalTheoryScoreList = new List<float>();
            var studentClassFinalPracticeScoreList = new List<float>();

            foreach (var programSyllabus in classStudent.TrainingClass.TrainingProgram.ProgramSyllabus)
            {
                // quiz 15%
                // assignment 15%
                // final 70%
                // final theory 40%
                // final practice 60%
                // (6 + 10)/2 x 15% + 10 x 15% + 10 x 70% = 1.2 + 1.5 + 7 = 9.7
                programSyllabus.Syllabus.QuizScheme = 15;
                var scoreQuizList = new List<float>();
                var scoreAssignmentList = new List<float>();
                var scoreFinalTheoryList = new List<float>();
                var scoreFinalPracticeList = new List<float>();
                if (classStudent.TrainingClass?.TrainingProgram == null) continue;
                foreach (var testAssessment in programSyllabus.Syllabus.TestAssessments)
                {
                    if (testAssessment.Score == null) continue;
                    switch (testAssessment.TestAssessmentType)
                    {
                        case TestAssessmentType.Quiz:
                            scoreQuizList.Add(testAssessment.Score.Value);
                            studentClassQuizScoreList.Add(testAssessment.Score.Value);
                            break;
                        case TestAssessmentType.Assignment:
                            scoreAssignmentList.Add(testAssessment.Score.Value);
                            studentClassAssignmentScoreList.Add(testAssessment.Score.Value);
                            break;
                        case TestAssessmentType.FinalTheory:
                            scoreFinalTheoryList.Add(testAssessment.Score.Value);
                            studentClassFinalTheoryScoreList.Add(testAssessment.Score.Value);
                            break;
                        case TestAssessmentType.FinalPractice:
                            scoreFinalPracticeList.Add(testAssessment.Score.Value);
                            studentClassFinalPracticeScoreList.Add(testAssessment.Score.Value);
                            break;
                        default:
                            break;
                    }
                }
                var quizAvg = scoreQuizList.Average();
                var assignmentAvg = scoreAssignmentList.Average();
                var finalTheoryAvg = scoreFinalTheoryList.Average();
                var finalPracticeAvg = scoreFinalPracticeList.Average();

                var syllabusGPA = quizAvg * programSyllabus.Syllabus.QuizScheme +
                    assignmentAvg * programSyllabus.Syllabus.AssignmentScheme +
                    (finalPracticeAvg * programSyllabus.Syllabus.FinalTheoryScheme +
                        finalPracticeAvg * programSyllabus.Syllabus.FinalPracticeScheme) * programSyllabus.Syllabus.FinalScheme;

                syllabusGPAList.Add(syllabusGPA);
            }

            studentProgressList.Add(new StudentProgressDTO()
            {
                ClassId = classStudent.TrainingClassId,
                ClassName = classStudent.TrainingClass.ClassName,
                StudentQuizAvg = studentClassQuizScoreList.Average(),
                StudentAssignmentAvg = studentClassAssignmentScoreList.Average(),
                StudentFinalTheoryAvg = studentClassFinalTheoryScoreList.Average(),
                StudentFinalPracticeAvg = studentClassFinalPracticeScoreList.Average(),
                StudentGPA = syllabusGPAList.Average(),
                ClassGPA = 8.9f
            });
        }

        var result = new Pagination<StudentProgressDTO>()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Items = studentProgressList
        };

        return result;
    }
}
