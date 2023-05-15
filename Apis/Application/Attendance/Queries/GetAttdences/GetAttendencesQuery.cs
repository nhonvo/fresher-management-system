using Application.Attendances.DTOs;
using Application.Commons;
using AutoMapper;
using MediatR;

namespace Application.Attendances.Queries.GetAttdences
{

    public record AttendancesQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<AttendanceDTO>>;

    public class AttdendenceHandler : IRequestHandler<AttendancesQuery, Pagination<AttendanceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttdendenceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<AttendanceDTO>> Handle(AttendancesQuery request, CancellationToken cancellationToken)
        {
            var attendances = await _unitOfWork.AttendanceRepository.ToPagination(request.PageIndex, request.PageSize);
            var result = _mapper.Map<Pagination<AttendanceDTO>>(attendances);
            return result;
        }
    }

}



