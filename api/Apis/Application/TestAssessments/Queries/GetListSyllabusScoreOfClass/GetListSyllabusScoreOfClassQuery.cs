using Application.Common.Exceptions;
using Application.Commons;
using Application.ViewModels.TestAssessmentViewModels;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.TestAssessments.Queries.GetListSyllabusScoreOfClass
{
    public class GetListSyllabusScoreOfClassQuery : IRequest<Pagination<GetListSyllabusScoreOfClassViewModel>>
    {
        public int Id { get; set; }
        public int? studentId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetListSyllabusScoreOfClassHandler : IRequestHandler<GetListSyllabusScoreOfClassQuery, Pagination<GetListSyllabusScoreOfClassViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetListSyllabusScoreOfClassHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pagination<GetListSyllabusScoreOfClassViewModel>> Handle(GetListSyllabusScoreOfClassQuery request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            int? studentId = request.studentId;
            int pageIndex = request.PageIndex;
            int pageSize = request.PageSize;

            Expression<Func<TestAssessment, bool>> filter = studentId == null ? x => x.TrainingClassId == id && x.Score != null :
               x => x.AttendeeId == studentId && x.TrainingClassId == id && x.Score != null;
            var scoreByTestType = await _unitOfWork.TestAssessmentRepository.GetFinalScoreAsync(filter);
            var classFinalSyllabusScore = scoreByTestType.GroupBy(ta => new { ta.AttendeeId, ta.SyllabusId }).Select(group => new GetListSyllabusScoreOfClassViewModel
            {
                AttendeeId = group.Key.AttendeeId,
                SyllabusId = group.Key.SyllabusId,
                FinalSyllabusScore = group.Sum(ta => ta.AverageScore * ta.SyllabusScheme) / group.Sum(ta => ta.SyllabusScheme) ?? 0,
                ListAssessment = scoreByTestType.Where(x => x.SyllabusId == group.Key.SyllabusId && x.AttendeeId == group.Key.AttendeeId).ToList()
            }).OrderBy(x => x.AttendeeId).ToList();
            var count = classFinalSyllabusScore.Count();
            if (pageSize != 0)
            {
                classFinalSyllabusScore = classFinalSyllabusScore
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();
            }

            var result = new Pagination<GetListSyllabusScoreOfClassViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = classFinalSyllabusScore
            };
            return result ?? throw new NotFoundException("There is no test assessment for this class");
        }
    }
}
