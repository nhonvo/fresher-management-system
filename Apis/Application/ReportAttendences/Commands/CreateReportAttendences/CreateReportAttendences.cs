using Application.Common.Exceptions;
using Application.Interfaces;
using Application.ReportAttendences.DTO;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.ReportAttendances.Commands.CreateReportAttendances
{
    public record CreateReportAttendancesCommand : IRequest<ReportAttendanceDTO>
    {
        public string Reason { get; set; }
        public DateTime expectedDates { get; set; }
        public string StudentId { get; set; }
    }
    public class CreateReportAttendancesHandler : IRequestHandler<CreateReportAttendancesCommand, ReportAttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;


        public CreateReportAttendancesHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
        }
        public async Task<ReportAttendanceDTO> Handle(CreateReportAttendancesCommand request, CancellationToken cancellationToken)
        {
            var reportAttendance = _mapper.Map<ReportAttendance>(request);
            reportAttendance.IsDeleted = false;
            reportAttendance.CreatedBy = _claimService.CurrentUserId;
            reportAttendance.CreationDate = _currentTime.GetCurrentTime();
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ReportAttendanceRepository.AddAsync(reportAttendance);
            });
            var result = _mapper.Map<ReportAttendanceDTO>(reportAttendance);
            return result ?? throw new NotFoundException("reportAttendance not found");
        }
    }
}
