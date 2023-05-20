using Application.Commons;
using Application.Attendances.DTO;
using AutoMapper;
using MediatR;

namespace Application.Attendances.Queries.GetAttendanceRequest
{
    public record GetAttendanceRequestQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<AttendanceDTO>>;
    public class GetAttendanceRequestHandler : IRequestHandler<GetAttendanceRequestQuery, Pagination<AttendanceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAttendanceRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<AttendanceDTO>> Handle(GetAttendanceRequestQuery request, CancellationToken cancellationToken)
        {
            var Attendance = await _unitOfWork.AttendanceRepository.ToPagination(request.PageIndex, request.PageSize);

            var result = _mapper.Map<Pagination<AttendanceDTO>>(Attendance);

            return result;
        }
    }
}
