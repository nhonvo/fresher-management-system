using Application.Commons;
using Application.ReportAttendences.DTO;
using AutoMapper;
using MediatR;

namespace Application.ReportAttendences.Queries.GetReportAttendence
{
    public record GetReportAttendenceQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<ReportAttendanceDTO>>;
    public class GetReportAttendenceHandler : IRequestHandler<GetReportAttendenceQuery, Pagination<ReportAttendanceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReportAttendenceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<ReportAttendanceDTO>> Handle(GetReportAttendenceQuery request, CancellationToken cancellationToken)
        {
            var reportAttendence = await _unitOfWork.ReportAttendanceRepository.ToPagination(request.PageIndex, request.PageSize);

            var result = _mapper.Map<Pagination<ReportAttendanceDTO>>(reportAttendence);

            return result;
        }
    }
}
