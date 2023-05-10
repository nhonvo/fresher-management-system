using Application.Account.DTOs;
using Application.Attendences.DTO;
using Application.Common.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Attendences.Queries.GetAttendanceById
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
            var attendence = await _unitOfWork.AttendanceRepository.GetByIdAsync(request.id);

            var result = _mapper.Map<AttendanceDTO>(attendence);

            return result ?? throw new NotFoundException("Class not found");

        }
    }
}
