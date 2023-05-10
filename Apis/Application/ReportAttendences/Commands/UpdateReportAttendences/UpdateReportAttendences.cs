using Application.Class.DTO;
using Application.Common.Exceptions;
using Application.ReportAttendences.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.ReportAttendences.Commands.UpdateReportAttendences
{
    public record UpdateReportAttendencesCommand : IRequest<ReportAttendenceDTO> 
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public StatusAttendance statusAttendance { get; set; } = StatusAttendance.Waiting;
        public DateTime expectedDates { get; set; }
        public string StudentId { get; set; }
    }
    public class UpdateReportAttendencesHandler : IRequestHandler<UpdateReportAttendencesCommand, ReportAttendenceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateReportAttendencesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReportAttendenceDTO> Handle(UpdateReportAttendencesCommand request, CancellationToken cancellationToken)
        {
            var reportAttendence = await _unitOfWork.ReportAttendenceRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (reportAttendence == null)
                throw new NotFoundException("reportAttendence not found");
            reportAttendence = _mapper.Map<ReportAttendence>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ReportAttendenceRepository.Update(reportAttendence);
            });
            var result = _mapper.Map<ReportAttendenceDTO>(reportAttendence);
            return result ?? throw new NotFoundException("Can not update reportAttendence");
        }
    }
}
