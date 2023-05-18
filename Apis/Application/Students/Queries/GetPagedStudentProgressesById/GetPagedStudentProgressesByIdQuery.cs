using Application.Commons;
using Application.Students.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.StudentProgresses.Queries.GetPagedStudentProgressesById;

public record GetPagedStudentProgressesByIdQuery : IRequest<Pagination<StudentProgressDTO>>
{
    public int Id { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPagedStudentProgressesByIdHandler : IRequestHandler<GetPagedStudentProgressesByIdQuery, Pagination<StudentProgressDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedStudentProgressesByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<StudentProgressDTO>> Handle(GetPagedStudentProgressesByIdQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<ClassStudent>, IIncludableQueryable<ClassStudent, object>>? include = null;
        if (request.Id > 0)
        {
            include = x => x
                .Include(x => x.TrainingClass)
                    .ThenInclude(x => x.TrainingProgram)
                    .ThenInclude(x => x.ProgramSyllabus)
                    .ThenInclude(x => x.Syllabus);
        }
        var items = await _unitOfWork.ClassStudentRepository.ToPagination(
            filter: x => x.UserId == request.Id,
            include: include,
            pageIndex: request.PageIndex,
            pageSize: request.PageSize);

        var studentProgressList = new List<StudentProgressDTO>();

        foreach (var classStudent in items.Items)
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
                ClassName = classStudent.TrainingClass.Name,
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
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Items = studentProgressList
        };

        return result;
    }
}
