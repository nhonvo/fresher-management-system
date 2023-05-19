using Application.Common.Exceptions;
using Application.ReportAttendances.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.ReportAttendances.Commands.UpdateReportAttendances
{
    public record UpdateReportAttendancesCommand : IRequest<AttendanceDTO>
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? Reason { get; set; }
        public StatusAttendance? AttendanceStatus { get; set; }
        public DateTime Day { get; set; }
    }
    public class UpdateReportAttendancesHandler : IRequestHandler<UpdateReportAttendancesCommand, AttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateReportAttendancesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AttendanceDTO> Handle(UpdateReportAttendancesCommand request, CancellationToken cancellationToken)
        {
            var reportAttendance = await _unitOfWork.ReportAttendanceRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (reportAttendance == null)
                throw new NotFoundException("reportAttendance not found");
            reportAttendance = _mapper.Map<Attendance>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ReportAttendanceRepository.Update(reportAttendance);
            });
            var result = _mapper.Map<AttendanceDTO>(reportAttendance);
            return result ?? throw new NotFoundException("Can not update reportAttendance");
        }
    }
}
