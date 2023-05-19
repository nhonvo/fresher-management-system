using Application.Commons;
using Application.Attendances.DTO;
using AutoMapper;
using MediatR;

namespace Application.Attendances.Queries.GetAttendance
{
    public record GetAttendanceQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<AttendanceDTO>>;
    public class GetAttendanceHandler : IRequestHandler<GetAttendanceQuery, Pagination<AttendanceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAttendanceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<AttendanceDTO>> Handle(GetAttendanceQuery request, CancellationToken cancellationToken)
        {
            var Attendance = await _unitOfWork.AttendanceRepository.ToPagination(request.PageIndex, request.PageSize);

            var result = _mapper.Map<Pagination<AttendanceDTO>>(Attendance);

            return result;
        }
    }
}
