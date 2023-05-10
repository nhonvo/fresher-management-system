using Application.Attendances.DTOs;
using Application.Commons;
using AutoMapper;
using Domain.Aggregate.AppResult;
using MediatR;

namespace Application.Attendances.Queries.GetAttendances
{

    public record AttendanceQuery(int pageIndex = 0, int pageSize = 10) : IRequest<ApiResult<Pagination<AttendanceDTO>>>;

    public class AttendanceHandler : IRequestHandler<AttendanceQuery, ApiResult<Pagination<AttendanceDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttendanceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult<Pagination<AttendanceDTO>>> Handle(AttendanceQuery request, CancellationToken cancellationToken)
        {
            var attendance = await _unitOfWork.AttendanceRepository.ToPagination(request.pageIndex, request.pageSize);
            var result = _mapper.Map<Pagination<AttendanceDTO>>(attendance);
            return new ApiSuccessResult<Pagination<AttendanceDTO>>(result);
        }
    }
}



