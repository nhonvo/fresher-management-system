using Application.ApproveRequests.DTOs;
using Application.Common.Exceptions;
using Application.Emails.Commands.SendMailRequestJoinClass;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.ApproveRequests.Commands.CreateRequestCurrentUser
{
    public record CreateRequestCurrentUserCommand : IRequest<ApproveRequestRelatedDTO>
    {
        public int ClassId { get; set; }
    }
    public class CreateRequestCurrentUserHandler : IRequestHandler<CreateRequestCurrentUserCommand, ApproveRequestRelatedDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IClaimService _claimService;
        public CreateRequestCurrentUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
            _claimService = claimService;
        }
        public async Task<ApproveRequestRelatedDTO> Handle(CreateRequestCurrentUserCommand request, CancellationToken cancellationToken)
        {
            if (await CheckExist(_claimService.CurrentUserId, request.ClassId))
            {
                throw new TransactionException("Approve request exists for class ");
            }
            var approved = _mapper.Map<ApproveRequest>(request);
            approved.StudentId = _claimService.CurrentUserId;
            
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ApproveRequestRepository.AddAsync(approved);
            });
            await _mediator.Send(new SendMailRequestJoinClassCommand(request.ClassId));
            var result = _mapper.Map<ApproveRequestRelatedDTO>(approved);
            return result;
        }
        public async Task<bool> CheckExist(int studentId, int classId)
            => await _unitOfWork.ApproveRequestRepository.AnyAsync(
                x => x.StudentId == studentId
                     && x.ClassId == classId
                     && (x.Status == StatusApprove.Waiting || x.Status == StatusApprove.Approve));
    }
}
