using Application.Attendances.DTO;
using Application.Commons;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Attendances.Queries.GetAttendanceEachClass
{
    public record GetAttendanceEachClassQuery(int id, int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<AttendanceRelatedDTO>>;
    public class AttendanceEachClassHandler : IRequestHandler<GetAttendanceEachClassQuery, Pagination<AttendanceRelatedDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttendanceEachClassHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<AttendanceRelatedDTO>> Handle(GetAttendanceEachClassQuery request, CancellationToken cancellationToken)
        {
            var classs = await _unitOfWork.ClassTrainerRepository.ToPagination(0, 100);

            var attendance = await _unitOfWork.AttendanceRepository.GetAsync(
                filter: x => x.AttendanceStatus == StatusAttendance.Absent && x.ClassStudent.TrainingClassId == request.id,
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
