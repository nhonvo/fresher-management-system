using Application.Common.Exceptions;
using Application.ReportAttendances.DTO;
using AutoMapper;
using MediatR;

namespace Application.ReportAttendances.Queries.GetreportAttendanceById
{
    public record GetreportAttendanceByIdQuery(int id) : IRequest<ReportAttendanceDTO>;

    public class GetreportAttendanceByIdHandler : IRequestHandler<GetreportAttendanceByIdQuery, ReportAttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetreportAttendanceByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReportAttendanceDTO> Handle(GetreportAttendanceByIdQuery request, CancellationToken cancellationToken)
        {
            var reportAttendance = await _unitOfWork.ReportAttendanceRepository.GetByIdAsync(request.id);

            var result = _mapper.Map<ReportAttendanceDTO>(reportAttendance);

            return result ?? throw new NotFoundException("Class not found");
        }
    }

}
