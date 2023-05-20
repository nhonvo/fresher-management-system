using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Attendances.DTO;
using AutoMapper;
using MediatR;

namespace Application.Attendances.Commands.DeleteAttendances
{
    public record DeleteAttendancesCommand(int Id) : IRequest<AttendanceDTO>;

    public class DeleteAttendancesHandler : IRequestHandler<DeleteAttendancesCommand, AttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;


        public DeleteAttendancesHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
        }
        public async Task<AttendanceDTO> Handle(DeleteAttendancesCommand request, CancellationToken cancellationToken)
        {
            var Attendance = await _unitOfWork.AttendanceRepository.GetByIdAsync(request.Id);
            Attendance.DeletionDate = _currentTime.GetCurrentTime();
            Attendance.IsDeleted = true;
            Attendance.DeleteBy = _claimService.CurrentUserId;
            Attendance.ModificationBy = _claimService.CurrentUserId;
            Attendance.ModificationDate = _currentTime.GetCurrentTime();
            if (Attendance == null)
                throw new NotFoundException("Attendance not found");
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.AttendanceRepository.Update(Attendance);
            });
            var result = _mapper.Map<AttendanceDTO>(Attendance);
            return result ?? throw new NotFoundException("Can not delete class");
        }
    }
}
