using Application.ApproveRequests.DTOs;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Students.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ApproveRequests.Commands.AllowRequest
{
    public record AllowRequestCommand(int studentId, int classId, bool allow) : IRequest<ApproveRequestDTO>;
    public class AllowRequestHandler : IRequestHandler<AllowRequestCommand, ApproveRequestDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public AllowRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
        }
        public async Task<ApproveRequestDTO> Handle(AllowRequestCommand request, CancellationToken cancellationToken)
        {
            var studentExist = await CheckStudentExitsAsync(request.studentId);
            if (studentExist)
                throw new NotFoundException("Can not found student!!");

            var classExist = await CheckClassExitsAsync(request.classId);
            if (classExist)
                throw new NotFoundException("Can not found class!!");
            var approved = await _unitOfWork.ApproveRequestRepository.FirstOrdDefaultAsync(
                filter: x => x.ClassId == request.classId && x.StudentId == request.studentId,
                include: x => x.Include(x => x.TrainingClass).Include(x => x.Student));

            if (!request.allow)
                approved.Status = StatusApprove.Reject;
            approved.Status = StatusApprove.Approve;
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ApproveRequestRepository.Update(approved);
            });
            var result = _mapper.Map<ApproveRequestDTO>(approved);
            return result;
        }
        private async Task<bool> CheckStudentExitsAsync(int id)
            => await _unitOfWork.UserRepository.GetByIdAsyncAsNoTracking(id) == null ? true : false;
        private async Task<bool> CheckClassExitsAsync(int id)
            => await _unitOfWork.ClassRepository.GetByIdAsyncAsNoTracking(id) == null ? true : false;
    }
}
