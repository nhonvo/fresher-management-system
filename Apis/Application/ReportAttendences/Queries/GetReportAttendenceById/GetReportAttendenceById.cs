using Application.Common.Exceptions;
using Application.ReportAttendances.DTO;
using AutoMapper;
using MediatR;

namespace Application.ReportAttendances.Queries.GetreportAttendanceById
{
    public record GetreportAttendanceByIdQuery(int id) : IRequest<AttendanceDTO>;

    public class GetreportAttendanceByIdHandler : IRequestHandler<GetreportAttendanceByIdQuery, AttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetreportAttendanceByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AttendanceDTO> Handle(GetreportAttendanceByIdQuery request, CancellationToken cancellationToken)
        {
            var reportAttendance = await _unitOfWork.ReportAttendanceRepository.GetByIdAsync(request.id);

            var result = _mapper.Map<AttendanceDTO>(reportAttendance);

            return result ?? throw new NotFoundException("Class not found");
        }
    }

}
