using Application.Commons;
using Application.ReportAttendances.DTO;
using AutoMapper;
using MediatR;

namespace Application.ReportAttendances.Queries.GetreportAttendance
{
    public record GetreportAttendanceQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<ReportAttendanceDTO>>;
    public class GetreportAttendanceHandler : IRequestHandler<GetreportAttendanceQuery, Pagination<ReportAttendanceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetreportAttendanceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<ReportAttendanceDTO>> Handle(GetreportAttendanceQuery request, CancellationToken cancellationToken)
        {
            var reportAttendance = await _unitOfWork.ReportAttendanceRepository.ToPagination(request.PageIndex, request.PageSize);

            var result = _mapper.Map<Pagination<ReportAttendanceDTO>>(reportAttendance);

            return result;
        }
    }
}
