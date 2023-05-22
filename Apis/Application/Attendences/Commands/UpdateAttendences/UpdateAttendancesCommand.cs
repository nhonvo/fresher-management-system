using Application.Attendances.DTO;
using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Attendances.Commands.UpdateAttendances
{
    public record UpdateAttendancesCommand : IRequest<AttendanceDTO>
    {
        public int Id { get; set; }
        public string? Reason { get; set; }
        public StatusAttendance? AttendanceStatus { get; set; }
        public DateTime Day { get; set; }
    }
    public class UpdateAttendancesHandler : IRequestHandler<UpdateAttendancesCommand, AttendanceDTO>
    {  private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;


        public UpdateAttendancesHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
        }
        public async Task<AttendanceDTO> Handle(UpdateAttendancesCommand request, CancellationToken cancellationToken)
        {
            var Attendance = await _unitOfWork.AttendanceRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (Attendance == null)
            {

                throw new NotFoundException("Attendance not found");
            }
            Attendance = _mapper.Map<Attendance>(request);
            Attendance.StudentId = _claimService.CurrentUserId;
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.AttendanceRepository.Update(Attendance);
            });
            var result = _mapper.Map<AttendanceDTO>(Attendance);
            return result ?? throw new NotFoundException("Can not update Attendance");
        }
    }
}
