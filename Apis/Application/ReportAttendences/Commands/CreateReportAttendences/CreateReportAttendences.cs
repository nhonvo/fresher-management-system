using Application.Class.DTO;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.ReportAttendences.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReportAttendences.Commands.CreateReportAttendences
{
    public record CreateReportAttendencesCommand : IRequest<ReportAttendenceDTO>
    {
        public string Reason { get; set; }
        public DateTime expectedDates { get; set; }
        public string StudentId { get; set; }
    }
    public class CreateReportAttendencesHandler : IRequestHandler<CreateReportAttendencesCommand, ReportAttendenceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;


        public CreateReportAttendencesHandler(IUnitOfWork unitOfWork, IMapper mapper , IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService
        }
        public async Task<ReportAttendenceDTO> Handle(CreateReportAttendencesCommand request, CancellationToken cancellationToken)
        {
            var reportAttendance = _mapper.Map<ReportAttendence>(request);
            reportAttendance.
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ReportAttendenceRepository.AddAsync(reportAttendance);
            });
            var result = _mapper.Map<ReportAttendenceDTO>(reportAttendance);
            return result ?? throw new NotFoundException("reportAttendance not found");
        }
    }
}
