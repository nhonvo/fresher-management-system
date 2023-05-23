using Application.Attendances.DTO;
using Application.Commons;
using AutoMapper;
using MediatR;

namespace Application.Attendances.Queries.GetAttendanceRequest
{
    public record GetAttendanceRequestQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<AttendanceRelatedDTO>>;
    public class GetAttendanceRequestHandler : IRequestHandler<GetAttendanceRequestQuery, Pagination<AttendanceRelatedDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAttendanceRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<AttendanceRelatedDTO>> Handle(GetAttendanceRequestQuery request, CancellationToken cancellationToken)
        {
            var Attendance = await _unitOfWork.AttendanceRepository.GetAsync(pageIndex: request.PageIndex, pageSize: request.PageSize);

            var result = _mapper.Map<Pagination<AttendanceRelatedDTO>>(Attendance);

            return result;
        }
    }
}
