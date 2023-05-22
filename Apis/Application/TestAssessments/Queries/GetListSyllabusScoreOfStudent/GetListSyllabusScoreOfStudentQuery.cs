using Application.Common.Exceptions;
using Application.Commons;
using Application.ViewModels.TestAssessmentViewModels;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.TestAssessments.Queries.GetListSyllabusScoreOfStudent
{
    public class GetListSyllabusScoreOfStudentQuery : IRequest<Pagination<GetListSyllabusScoreOfStudentViewModel>>
    {
        public int Id { get; set; }
        public int? ClassId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetListSyllabusScoreOfStudentQueryHandler : IRequestHandler<GetListSyllabusScoreOfStudentQuery, Pagination<GetListSyllabusScoreOfStudentViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetListSyllabusScoreOfStudentQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pagination<GetListSyllabusScoreOfStudentViewModel>> Handle(GetListSyllabusScoreOfStudentQuery request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            int? classId = request.ClassId;
            int pageIndex = request.PageIndex;
            int pageSize = request.PageSize;

            Expression<Func<TestAssessment, bool>> filter = classId == null ? x => x.AttendeeId == id && x.Score != null :
                x => x.AttendeeId == id && x.TrainingClassId == classId && x.Score != null;
            var scoreByTestType = await _unitOfWork.TestAssessmentRepository.GetFinalScoreAsync(filter);
            var studentFinalSyllabusScore = scoreByTestType.GroupBy(ta => new { ta.SyllabusId, ta.TrainingClassId }).Select(group => new GetListSyllabusScoreOfStudentViewModel
            {
                SyllabusId = group.Key.SyllabusId,
                SyllabusName = _unitOfWork.SyllabusRepository.GetByIdAsync(group.Key.SyllabusId).Result.Name,
                TrainingClassId = group.Key.TrainingClassId,
                FinalSyllabusScore = group.Sum(ta => ta.AverageScore * ta.SyllabusScheme) / group.Sum(ta => ta.SyllabusScheme) ?? 0,
                ListAssessment = scoreByTestType.Where(x => x.SyllabusId == group.Key.SyllabusId && x.TrainingClassId == group.Key.TrainingClassId).ToList()
            }).ToList();
            var count = studentFinalSyllabusScore.Count();

            if (pageSize != 0)
            {
                studentFinalSyllabusScore = studentFinalSyllabusScore
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize).ToList();
            }

            var result = new Pagination<GetListSyllabusScoreOfStudentViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = studentFinalSyllabusScore
            };
            return result ?? throw new NotFoundException("There is no test assessment for this student");
        }
    }
}
