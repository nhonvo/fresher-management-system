using Application.ApproveRequests.DTOs;
using Application.Commons;
using AutoMapper;
using MediatR;

namespace Application.ApproveRequests.GetApprove
{
    public record GetApproveQuery(int pageIndex = 0, int pageSize = 10) : IRequest<Pagination<ApproveRequestDTO>>;

    public class GetApproveHandler : IRequestHandler<GetApproveQuery, Pagination<ApproveRequestDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetApproveHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<ApproveRequestDTO>> Handle(GetApproveQuery request, CancellationToken cancellationToken)
        {
            var approveRequest = await _unitOfWork.ApproveRequestRepository.GetAsync(pageIndex: request.pageIndex, pageSize: request.pageSize);

            var result = _mapper.Map<Pagination<ApproveRequestDTO>>(approveRequest);

            return result;
        }
    }
}


