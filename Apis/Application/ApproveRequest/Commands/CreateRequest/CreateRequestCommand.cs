using Application.ApproveRequests.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.ApproveRequests.Commands.CreateRequest
{
    public record CreateRequestCommand : IRequest<ApproveRequestDTO>
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }

    }
    public class CreateRequestHandler : IRequestHandler<CreateRequestCommand, ApproveRequestDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApproveRequestDTO> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var approved = _mapper.Map<ApproveRequest>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ApproveRequestRepository.AddAsync(approved);
            });
            var result = _mapper.Map<ApproveRequestDTO>(approved);
            return result ?? throw new InvalidOperationException("Cannot create request!!");
        }
    }
}
