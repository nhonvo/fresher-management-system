using Application.Class.DTO;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.ReportAttendences.DTO;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReportAttendences.Commands.DeleteReportAttendences
{
    public record DeleteReportAttendencesCommand(int Id) : IRequest<ReportAttendenceDTO>;

    public class DeleteReportAttendencesHandler : IRequestHandler<DeleteReportAttendencesCommand, ReportAttendenceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;


        public DeleteReportAttendencesHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
        }
        public async Task<ReportAttendenceDTO> Handle(DeleteReportAttendencesCommand request, CancellationToken cancellationToken)
        {
            var reportAttendence = await _unitOfWork.ReportAttendenceRepository.GetByIdAsync(request.Id);
            reportAttendence.DeletionDate = _currentTime.GetCurrentTime();
            reportAttendence.IsDeleted = true;
            reportAttendence.DeleteBy = _claimService.CurrentUserId;
            reportAttendence.ModificationBy = _claimService.CurrentUserId;
            reportAttendence.ModificationDate = _currentTime.GetCurrentTime();
            if (reportAttendence == null)
                throw new NotFoundException("reportAttendence not found");
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ReportAttendenceRepository.Update(reportAttendence);
            });
            var result = _mapper.Map<ReportAttendenceDTO>(reportAttendence);
            return result ?? throw new NotFoundException("Can not delete class");
        }
    }
}
