using Application.Common.Exceptions;
using Application.ReportAttendances.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.ReportAttendances.Commands.UpdateReportAttendances
{
    public record UpdateReportAttendancesCommand : IRequest<ReportAttendanceDTO>
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public StatusAttendance statusAttendance { get; set; } = StatusAttendance.Waiting;
        public DateTime expectedDates { get; set; }
        public string StudentId { get; set; }
    }
    public class UpdateReportAttendancesHandler : IRequestHandler<UpdateReportAttendancesCommand, ReportAttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateReportAttendancesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReportAttendanceDTO> Handle(UpdateReportAttendancesCommand request, CancellationToken cancellationToken)
        {
            var reportAttendance = await _unitOfWork.ReportAttendanceRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (reportAttendance == null)
                throw new NotFoundException("reportAttendance not found");
            reportAttendance = _mapper.Map<ReportAttendance>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ReportAttendanceRepository.Update(reportAttendance);
            });
            var result = _mapper.Map<ReportAttendanceDTO>(reportAttendance);
            return result ?? throw new NotFoundException("Can not update reportAttendance");
        }
    }
}
