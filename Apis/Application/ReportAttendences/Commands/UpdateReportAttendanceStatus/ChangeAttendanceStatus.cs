using Application.Common.Exceptions;
using Application.ReportAttendances.DTO;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.ReportAttendances.Commands.UpdateReportAttendanceStatus
{
    public record ChangeAttendanceStatusCommand : IRequest<AttendanceDTO>
    {
        public int Id { get; set; }
        public StatusAttendance NewStatus { get; set; }
    }
    public class ChangeAttendanceStatusHandler : IRequestHandler<ChangeAttendanceStatusCommand, AttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChangeAttendanceStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AttendanceDTO> Handle(ChangeAttendanceStatusCommand request, CancellationToken cancellationToken)
        {
            var report = await _unitOfWork.ReportAttendanceRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (report == null)
            {
                throw new NotFoundException("Report Not Found");
            }
            report.AttendanceStatus = request.NewStatus;
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ReportAttendanceRepository.Update(report);
            });
            var result = _mapper.Map<AttendanceDTO>(report);
            return result;
        }
    }
}
