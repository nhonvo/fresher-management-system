using Application.ApproveRequests.DTOs;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.ApproveRequests.Commands.CreateRequest
{
    public record CreateRequestCommand : IRequest<ApproveRequestRelatedDTO>
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }

    }
    public class CreateRequestHandler : IRequestHandler<CreateRequestCommand, ApproveRequestRelatedDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApproveRequestRelatedDTO> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            if (await CheckExist(request.StudentId, request.ClassId))
                throw new TransactionException("Approve request exists for class ");
            var approved = _mapper.Map<ApproveRequest>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ApproveRequestRepository.AddAsync(approved);
            });
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
