using Application.Common.Exceptions;
using Application.Attendances.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Attendances.Queries.GetAttendanceById
{
    public record GetAttendanceByIdQuery(int id) : IRequest<AttendanceRelatedDTO>;

    public class GetAttendanceByIdHandler : IRequestHandler<GetAttendanceByIdQuery, AttendanceRelatedDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAttendanceByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AttendanceRelatedDTO> Handle(GetAttendanceByIdQuery request, CancellationToken cancellationToken)
        {
            var Attendance = await _unitOfWork.AttendanceRepository.FirstOrDefaultAsync(
                filter: x => x.Id == request.id,
                include: x => x.Include(x => x.Admin)
                               .Include(x => x.ClassStudent)
                               .ThenInclude(x => x.Student)
                               .Include(x => x.ClassStudent)
                               .ThenInclude(x => x.TrainingClass));

            var result = _mapper.Map<AttendanceRelatedDTO>(Attendance);

            return result ?? throw new NotFoundException("Class not found");
        }
    }

}
