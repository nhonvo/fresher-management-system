using Application.Attendances.DTO;
using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Attendances.Commands.CreateAttendances
{
    public record CreateAttendancesCommand : IRequest<AttendanceDTO>
    {
        public string Reason { get; set; }
        public DateTime expectedDates { get; set; }
        public string StudentId { get; set; }
    }
    public class CreateAttendancesHandler : IRequestHandler<CreateAttendancesCommand, AttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;


        public CreateAttendancesHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
        }
        public async Task<AttendanceDTO> Handle(CreateAttendancesCommand request, CancellationToken cancellationToken)
        {
            var Attendance = _mapper.Map<Attendance>(request);
            Attendance.IsDeleted = false;
            Attendance.CreatedBy = _claimService.CurrentUserId;
            Attendance.CreationDate = _currentTime.GetCurrentTime();
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.AttendanceRepository.AddAsync(Attendance);
            });
            var result = _mapper.Map<AttendanceDTO>(Attendance);
            return result ?? throw new NotFoundException("Attendance not found");
        }
    }
}
