using Application.Attendances.DTO;
using Application.Common.Exceptions;
using Application.Interfaces;
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
            var attendance = await _unitOfWork.AttendanceRepository.GetByIdAsync(request.Id);
            attendance.DeletionDate = _currentTime.GetCurrentTime();
            attendance.IsDeleted = true;
            attendance.DeleteBy = _claimService.CurrentUserId;
            attendance.ModificationBy = _claimService.CurrentUserId;
            attendance.ModificationDate = _currentTime.GetCurrentTime();
            if (attendance == null)
            {
                throw new NotFoundException("Attendance not found");
            }
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.AttendanceRepository.Update(attendance);
            });
            var result = _mapper.Map<AttendanceDTO>(attendance);
            return result ?? throw new NotFoundException("Can not delete class");
        }
    }
}
