using Application.Common.Exceptions;
using Application.Interfaces;
using Application.ReportAttendances.DTO;
using AutoMapper;
using MediatR;

namespace Application.ReportAttendances.Commands.DeleteReportAttendances
{
    public record DeleteReportAttendancesCommand(int Id) : IRequest<ReportAttendanceDTO>;

    public class DeleteReportAttendancesHandler : IRequestHandler<DeleteReportAttendancesCommand, ReportAttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;


        public DeleteReportAttendancesHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
        }
        public async Task<ReportAttendanceDTO> Handle(DeleteReportAttendancesCommand request, CancellationToken cancellationToken)
        {
            var reportAttendance = await _unitOfWork.ReportAttendanceRepository.GetByIdAsync(request.Id);
            reportAttendance.DeletionDate = _currentTime.GetCurrentTime();
            reportAttendance.IsDeleted = true;
            reportAttendance.DeleteBy = _claimService.CurrentUserId;
            reportAttendance.ModificationBy = _claimService.CurrentUserId;
            reportAttendance.ModificationDate = _currentTime.GetCurrentTime();
            if (reportAttendance == null)
                throw new NotFoundException("reportAttendance not found");
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ReportAttendanceRepository.Update(reportAttendance);
            });
            var result = _mapper.Map<ReportAttendanceDTO>(reportAttendance);
            return result ?? throw new NotFoundException("Can not delete class");
        }
    }
}
