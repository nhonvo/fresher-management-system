using Application.Common.Exceptions;
using Application.Attendances.DTO;
using AutoMapper;
using MediatR;

namespace Application.Attendances.Queries.GetAttendanceById
{
    public record GetAttendanceByIdQuery(int id) : IRequest<AttendanceDTO>;

    public class GetAttendanceByIdHandler : IRequestHandler<GetAttendanceByIdQuery, AttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAttendanceByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AttendanceDTO> Handle(GetAttendanceByIdQuery request, CancellationToken cancellationToken)
        {
            var Attendance = await _unitOfWork.AttendanceRepository.GetByIdAsync(request.id);

            var result = _mapper.Map<AttendanceDTO>(Attendance);

            return result ?? throw new NotFoundException("Class not found");
        }
    }

}
