using Application.Common.Exceptions;
using Application.Attendances.DTO;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Application.Attendances.Commands.UpdateAttendanceStatus
{
    public record ApproveAbsentCommand : IRequest<AttendanceDTO>
    {
        public int Id { get; set; }
        public StatusAttendance? AttendanceStatus { get; set; }
    }
    public class ChangeAttendanceStatusHandler : IRequestHandler<ApproveAbsentCommand, AttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;

        public ChangeAttendanceStatusHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime;
            _claimService = claimService;
        }
        public async Task<AttendanceDTO> Handle(ApproveAbsentCommand request, CancellationToken cancellationToken)
        {
            var attendance = await _unitOfWork.AttendanceRepository.FirstOrDefaultAsync(
                filter: x => x.Id == request.Id,
                include: x => x.Include(x => x.ClassStudent).ThenInclude(x => x.TrainingClass));
            if (attendance == null)
            {
                throw new NotFoundException("Attendance Not Found");
            }
            
            attendance.ModificationBy = _claimService.CurrentUserId;
            attendance.ModificationDate = _currentTime.GetCurrentTime();
            attendance.AdminId = _claimService.CurrentUserId;

            attendance.AttendanceStatus = request.AttendanceStatus;
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.AttendanceRepository.Update(attendance);
            });
            var result = _mapper.Map<AttendanceDTO>(attendance);
            result.ClassName = attendance.ClassStudent.TrainingClass.Name;
            return result;
        }
    }
}
