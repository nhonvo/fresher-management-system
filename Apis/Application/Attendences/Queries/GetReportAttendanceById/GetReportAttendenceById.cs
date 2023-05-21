using Application.Common.Exceptions;
using Application.Attendances.DTO;
using AutoMapper;
using MediatR;

namespace Application.Attendances.Queries.GetReportAttendanceById
{
    public record GetReportAttendanceByIdQuery(int id) : IRequest<AttendanceDTO>;

    public class GetReportAttendanceByIdHandler : IRequestHandler<GetReportAttendanceByIdQuery, AttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReportAttendanceByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AttendanceDTO> Handle(GetReportAttendanceByIdQuery request, CancellationToken cancellationToken)
        {
            var Attendance = await _unitOfWork.AttendanceRepository.GetByIdAsync(request.id);

            var result = _mapper.Map<AttendanceDTO>(Attendance);

            return result ?? throw new NotFoundException("Class not found");
        }
    }

}
