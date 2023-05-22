using Application.Attendances.DTO;
using Application.Commons;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Attendances.Queries.GetAttendanceFilterRequest
{
    public record GetAttendanceFilterRequestQuery(StatusAttendanceApprove? status = StatusAttendanceApprove.Pending, int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<AttendanceRelatedFilterDTO>>;
    public class GetAttendanceFilterRequestHandler : IRequestHandler<GetAttendanceFilterRequestQuery, Pagination<AttendanceRelatedFilterDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAttendanceFilterRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<AttendanceRelatedFilterDTO>> Handle(GetAttendanceFilterRequestQuery request, CancellationToken cancellationToken)
        {
            var attendance = await _unitOfWork.AttendanceRepository.GetAsync(
                filter: x => x.ApproveStatus == request.status,
                 include: x => x.Include(x => x.Admin)
                                .Include(x => x.ClassStudent)
                                .ThenInclude(x => x.Student)
                                .Include(x => x.ClassStudent)
                                .ThenInclude(x => x.TrainingClass),
                pageIndex: request.PageIndex,
                pageSize: request.PageSize);

            var result = _mapper.Map<Pagination<AttendanceRelatedFilterDTO>>(attendance);

            return result;
        }
    }
}
