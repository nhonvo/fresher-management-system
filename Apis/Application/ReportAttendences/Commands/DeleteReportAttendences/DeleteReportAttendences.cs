using Application.Class.DTO;
using Application.Common.Exceptions;
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
        public DeleteReportAttendencesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReportAttendenceDTO> Handle(DeleteReportAttendencesCommand request, CancellationToken cancellationToken)
        {
            var reportAttendence = await _unitOfWork.ReportAttendanceRepository.GetByIdAsync(request.Id);
            if (reportAttendence == null)
                throw new NotFoundException("reportAttendence not found");

            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ReportAttendanceRepository.Delete(reportAttendence);
            });
            var result = _mapper.Map<ReportAttendenceDTO>(reportAttendence);
            return result ?? throw new NotFoundException("Can not delete class");
        }
    }
}
