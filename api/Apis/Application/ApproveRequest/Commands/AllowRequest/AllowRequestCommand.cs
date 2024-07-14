using Application.ApproveRequests.Commands.SendMailApproveRequest;
using Application.ApproveRequests.DTOs;
using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Application.ApproveRequests.Commands.AllowRequest
{
    public record AllowRequestCommand(int id, bool allowJoin) : IRequest<ApproveResponseDTO>;
    public class AllowRequestHandler : IRequestHandler<AllowRequestCommand, ApproveResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        private readonly IMediator _mediator;
        public AllowRequestHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IClaimService claimService,
            ICurrentTime currentTime,
            IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
            _mediator = mediator;
        }
        public async Task<ApproveResponseDTO> Handle(AllowRequestCommand request, CancellationToken cancellationToken)
        {
            await CheckApprovalExistence(request.id);
            await CheckApproval(request.id);

            var approved = await GetApprovedRequest(request.id);
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                UpdateApprovalStatus(approved, request.allowJoin);
                await AddClassStudent(approved.ClassId, approved.StudentId);
            });

            var result = CreateApproveResponse(approved);
            await _mediator.Send(new SendMailApproveRequestCommand(request.id));
            return result;
        }

        private async Task CheckApprovalExistence(int requestId)
        {
            var exist = await CheckApproveRequestExits(requestId);
            if (exist is false)
                throw new NotFoundException("Cannot find approve request");
        }
        private async Task CheckApproval(int requestId)
        {
            var exist = await CheckApproveRequest(requestId);
            if (exist is true)
                throw new NotFoundException("Your approve request approved!!");
        }

        private async Task<ApproveRequest> GetApprovedRequest(int requestId)
        {
            return await _unitOfWork.ApproveRequestRepository.FirstOrDefaultAsync(
                filter: x => x.Id == requestId,
                include: x => x.Include(x => x.TrainingClass).Include(x => x.Student));
        }

        private void UpdateApprovalStatus(ApproveRequest approved, bool allowJoin)
        {
            approved.Status = allowJoin ? StatusApprove.Approve : StatusApprove.Reject;
            approved.ApproveBy = _claimService.CurrentUserId;
            _unitOfWork.ApproveRequestRepository.Update(approved);
        }

        private async Task AddClassStudent(int classId, int studentId)
        {
            var classStudent = new ClassStudent
            {
                TrainingClassId = classId,
                StudentId = studentId,
                CreationDate = _currentTime.GetCurrentTime(),
                CreatedBy = _claimService.CurrentUserId,
                GPA = 0
            };

            await _unitOfWork.ClassStudentRepository.AddAsync(classStudent);
        }

        private ApproveResponseDTO CreateApproveResponse(ApproveRequest approved)
        {
            var result = new ApproveResponseDTO
            {
                Status = approved.Status,
                StudentId = approved.StudentId,
                ClassId = approved.ClassId,
                ApprovedBy = _claimService.CurrentUserId,
            };

            return result;
        }

        private async Task<bool> CheckApproveRequestExits(int id)
            => await _unitOfWork.ApproveRequestRepository.AnyAsync(x => x.Id == id);
        private async Task<bool> CheckApproveRequest(int id)
            => await _unitOfWork.ApproveRequestRepository.AnyAsync(x => x.Id == id && x.Status == StatusApprove.Approve);
    }
}