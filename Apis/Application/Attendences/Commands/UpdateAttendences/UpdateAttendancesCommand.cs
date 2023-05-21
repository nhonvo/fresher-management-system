using Application.Attendances.DTO;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Attendances.Commands.UpdateAttendances
{
    public record UpdateAttendancesCommand : IRequest<AttendanceDTO>
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? Reason { get; set; }
        public StatusAttendance? AttendanceStatus { get; set; }
        public DateTime Day { get; set; }
    }
    public class UpdateAttendancesHandler : IRequestHandler<UpdateAttendancesCommand, AttendanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateAttendancesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AttendanceDTO> Handle(UpdateAttendancesCommand request, CancellationToken cancellationToken)
        {
            var Attendance = await _unitOfWork.AttendanceRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (Attendance == null)
                throw new NotFoundException("Attendance not found");
            Attendance = _mapper.Map<Attendance>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.AttendanceRepository.Update(Attendance);
            });
            var result = _mapper.Map<AttendanceDTO>(Attendance);
            return result ?? throw new NotFoundException("Can not update Attendance");
        }
    }
}
