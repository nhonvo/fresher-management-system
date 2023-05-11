using Application.Class.DTO;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.ReportAttendences.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.ReportAttendences.Commands.CreateReportAttendences
{
    public record CreateReportAttendencesCommand : IRequest<ReportAttendenceDTO>
    {
        public string Reason { get; set; }
        public DateTime expectedDates { get; set; }
        public string StudentId { get; set; }
    }
    public class CreateReportAttendencesHandler : IRequestHandler<CreateReportAttendencesCommand, ReportAttendenceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;


        public CreateReportAttendencesHandler(IUnitOfWork unitOfWork, IMapper mapper , IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
        }
        public async Task<ReportAttendenceDTO> Handle(CreateReportAttendencesCommand request, CancellationToken cancellationToken)
        {
            var reportAttendance = _mapper.Map<ReportAttendence>(request);
            reportAttendance.IsDeleted = false;
            reportAttendance.CreatedBy = _claimService.CurrentUserId;
            reportAttendance.CreationDate = _currentTime.GetCurrentTime();
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ReportAttendanceRepository.AddAsync(reportAttendance);
            });
            var result = _mapper.Map<ReportAttendenceDTO>(reportAttendance);
            return result ?? throw new NotFoundException("reportAttendance not found");
        }
    }
}
