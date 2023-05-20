using Application.Commons;
using Application.Attendances.DTO;
using AutoMapper;
using MediatR;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Attendances.Queries.SearchAttendanceRequest
{
    public record SearchAttendanceRequestQuery(
        string? searchString,
        SortType sortType = SortType.Ascending,
        int pageIndex = 0,
        int pageSize = 10) : IRequest<Pagination<AttendanceRelatedDTO>>;
    public class SearchAttendanceRequestHandler : IRequestHandler<SearchAttendanceRequestQuery, Pagination<AttendanceRelatedDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchAttendanceRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<AttendanceRelatedDTO>> Handle(SearchAttendanceRequestQuery request, CancellationToken cancellationToken)
        {
            var attendance = await _unitOfWork.AttendanceRepository.GetAsync<DateTime>(
                filter: x => x.Reason.Contains(request.searchString),
                include: x => x.Include(x => x.Admin)
                               .Include(x => x.ClassStudent)
                               .ThenInclude(x => x.Student)
                               .Include(x => x.ClassStudent)
                               .ThenInclude(x => x.TrainingClass),
                sortType: request.sortType,
                keySelectorForSort: x => x.Day,
                pageIndex: request.pageIndex,
                pageSize: request.pageSize);

            var result = _mapper.Map<Pagination<AttendanceRelatedDTO>>(attendance);
            return result;
        }
    }
}
