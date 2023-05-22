using Application.ApproveRequests.DTOs;
using Application.Commons;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ApproveRequests.GetApproveById
{

    public record GetApproveByIdQuery(int id) : IRequest<ApproveRequestRelatedDTO>;

    public class GetApproveByIdHandler : IRequestHandler<GetApproveByIdQuery, ApproveRequestRelatedDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetApproveByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApproveRequestRelatedDTO> Handle(GetApproveByIdQuery request, CancellationToken cancellationToken)
        {
            var approveRequest = await _unitOfWork.ApproveRequestRepository.FirstOrDefaultAsync(
                filter: x => x.Id == request.id,
                include: x => x.Include(x => x.Student)
                               .Include(x => x.Admin)
                               .Include(x => x.TrainingClass));

            var result = _mapper.Map<ApproveRequestRelatedDTO>(approveRequest);

            return result;
        }
    }
}


