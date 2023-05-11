using Application.Common.Exceptions;
using Application.ReportAttendences.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.ReportAttendences.Commands.UpdateReportAttendanceStatus
{
    public record ChangeAttendanceStatusCommand : IRequest<ReportAttendenceDTO>
    {
        public int Id { get; set; }
        public StatusAttendance NewStatus { get; set; }
    }
    public class ChangeAttendanceStatusHandler : IRequestHandler<ChangeAttendanceStatusCommand, ReportAttendenceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChangeAttendanceStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReportAttendenceDTO> Handle(ChangeAttendanceStatusCommand request, CancellationToken cancellationToken)
        {
            var report = await _unitOfWork.ReportAttendanceRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (report == null)
            {
                throw new NotFoundException("Report Not Found");
            }
            report.statusAttendance = request.NewStatus;
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ReportAttendanceRepository.Update(report);
            });
            var result = _mapper.Map<ReportAttendenceDTO>(report);
            return result;
        }
    }
}
