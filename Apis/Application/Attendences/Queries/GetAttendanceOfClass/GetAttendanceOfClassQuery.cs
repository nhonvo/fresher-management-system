using Application.Attendances.DTO;
using Application.Commons;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Attendances.Queries.GetAttendanceOfClass
{
    public record GetAttendanceOfClassQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<AttendanceRelatedDTO>>;
    public class SendAttendanceEachClassHandler : IRequestHandler<GetAttendanceOfClassQuery, Pagination<AttendanceRelatedDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SendAttendanceEachClassHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<AttendanceRelatedDTO>> Handle(GetAttendanceOfClassQuery request, CancellationToken cancellationToken)
        {
            var attendance = await _unitOfWork.AttendanceRepository.GetAsync(
                filter: x => x.AttendanceStatus == StatusAttendance.Absent,
                 include: x => x.Include(x => x.Admin)
                               .Include(x => x.ClassStudent)
                                    .ThenInclude(x => x.Student)
                               .Include(x => x.ClassStudent)
                                    .ThenInclude(x => x.TrainingClass)
                                        .ThenInclude(x => x.ClassTrainers)
                                            .ThenInclude(x => x.Trainer)
                               .Include(x => x.ClassStudent)
                                    .ThenInclude(x => x.TrainingClass)
                                        .ThenInclude(x => x.ClassAdmins)
                                            .ThenInclude(x => x.Admin),
                pageIndex: request.PageIndex,
                pageSize: request.PageSize);

            var result = _mapper.Map<Pagination<AttendanceRelatedDTO>>(attendance);

            return result;
        }
    }

}
