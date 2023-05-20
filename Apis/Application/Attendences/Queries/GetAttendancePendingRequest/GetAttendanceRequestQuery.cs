using Application.Commons;
using Application.Attendances.DTO;
using AutoMapper;
using MediatR;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Attendances.Queries.GetAttendancePendingRequest
{
    public record GetAttendancePendingRequestQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<AttendanceRelatedDTO>>;
    public class GetAttendancePendingRequestHandler : IRequestHandler<GetAttendancePendingRequestQuery, Pagination<AttendanceRelatedDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAttendancePendingRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<AttendanceRelatedDTO>> Handle(GetAttendancePendingRequestQuery request, CancellationToken cancellationToken)
        {
            var attendance = await _unitOfWork.AttendanceRepository.GetAsync(
                filter: x => x.ApproveStatus == StatusAttendanceApprove.Pending,
                 include: x => x.Include(x => x.Admin)
                               .Include(x => x.ClassStudent)
                               .ThenInclude(x => x.Student)
                               .Include(x => x.ClassStudent)
                               .ThenInclude(x => x.TrainingClass),
                pageIndex: request.PageIndex,
                pageSize: request.PageSize);

            var result = _mapper.Map<Pagination<AttendanceRelatedDTO>>(attendance);

            return result;
        }
    }
}
