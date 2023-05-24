using Application.Attendances.DTO;
using Application.Common.Exceptions;
using Application.Emails.Commands.SendMailApproveAbsent;
using Application.Interfaces;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Attendances.Commands.ApproveAbsent
{
    public record ApproveAbsentCommand : IRequest<AttendanceDTO>
    {
        public int Id { get; set; }
        public StatusAttendanceApprove AttendanceStatus { get; set; }
    }
    public class ChangeAttendanceStatusHandler : IRequestHandler<ApproveAbsentCommand, AttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        private readonly IMediator _mediator;

        public ChangeAttendanceStatusHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime;
            _claimService = claimService;
            _mediator = mediator;
        }
        public async Task<AttendanceDTO> Handle(ApproveAbsentCommand request, CancellationToken cancellationToken)
        {
            var attendance = await _unitOfWork.AttendanceRepository.FirstOrDefaultAsync(
                filter: x => x.Id == request.Id,
                include: x => x
                .Include(x => x.ClassStudent)
                    .ThenInclude(x => x.Student)
                .Include(x => x.ClassStudent)
                    .ThenInclude(x => x.TrainingClass));
            if (attendance == null)
            {
                throw new NotFoundException("Attendance Not Found");
            }

            attendance.ModificationBy = _claimService.CurrentUserId;
            attendance.ModificationDate = _currentTime.GetCurrentTime();
            attendance.AdminId = _claimService.CurrentUserId;

            attendance.ApproveStatus = request.AttendanceStatus;
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.AttendanceRepository.Update(attendance);
            });
            var result = _mapper.Map<AttendanceDTO>(attendance);
            result.ClassName = attendance.ClassStudent.TrainingClass.Name;
            await _mediator.Send(new SendMailApproveAbsentCommand(request.Id, attendance.ClassStudent.Student.Email, attendance.ClassStudent.Student.Name));
            return result;
        }
    }
}
