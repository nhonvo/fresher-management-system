using Application.Class.DTO;
using Application.Common.Exceptions;
using Application.ReportAttendences.DTO;
using AutoMapper;
using MediatR;

namespace Application.ReportAttendences.Queries.GetReportAttendenceById
{
    public record GetReportAttendenceByIdQuery(int id) : IRequest<ReportAttendenceDTO>;

    public class GetReportAttendenceByIdHandler : IRequestHandler<GetReportAttendenceByIdQuery, ReportAttendenceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReportAttendenceByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReportAttendenceDTO> Handle(GetReportAttendenceByIdQuery request, CancellationToken cancellationToken)
        {
            var ReportAttendence = await _unitOfWork.ReportAttendanceRepository.GetByIdAsync(request.id);

            var result = _mapper.Map<ReportAttendenceDTO>(ReportAttendence);

            return result ?? throw new NotFoundException("Class not found");
        }
    }

}
