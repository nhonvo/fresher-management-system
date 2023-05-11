using Application.Common.Exceptions;
using Application.ReportAttendences.DTO;
using AutoMapper;
using MediatR;

namespace Application.ReportAttendences.Queries.GetReportAttendenceById
{
    public record GetReportAttendenceByIdQuery(int id) : IRequest<ReportAttendanceDTO>;

    public class GetReportAttendenceByIdHandler : IRequestHandler<GetReportAttendenceByIdQuery, ReportAttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReportAttendenceByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReportAttendanceDTO> Handle(GetReportAttendenceByIdQuery request, CancellationToken cancellationToken)
        {
            var ReportAttendence = await _unitOfWork.ReportAttendanceRepository.GetByIdAsync(request.id);

            var result = _mapper.Map<ReportAttendanceDTO>(ReportAttendence);

            return result ?? throw new NotFoundException("Class not found");
        }
    }

}
