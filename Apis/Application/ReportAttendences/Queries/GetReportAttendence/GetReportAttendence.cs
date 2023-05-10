using Application.Class.DTO;
using Application.Commons;
using Application.ReportAttendences.DTO;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReportAttendences.Queries.GetReportAttendence
{
    public record GetReportAttendenceQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<ReportAttendenceDTO>>;
    public class GetReportAttendenceHandler : IRequestHandler<GetReportAttendenceQuery, Pagination<ReportAttendenceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReportAttendenceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<ReportAttendenceDTO>> Handle(GetReportAttendenceQuery request, CancellationToken cancellationToken)
        {
            var reportAttendence = await _unitOfWork.ReportAttendanceRepository.ToPagination(request.PageIndex, request.PageSize);

            var result = _mapper.Map<Pagination<ReportAttendenceDTO>>(reportAttendence);

            return result;
        }
    }
}
