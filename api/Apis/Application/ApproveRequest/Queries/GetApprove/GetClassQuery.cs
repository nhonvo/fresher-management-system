using Application.ApproveRequests.DTOs;
using Application.Commons;
using AutoMapper;
using MediatR;

namespace Application.ApproveRequests.GetApprove
{
    public record GetApproveQuery(int pageIndex = 0, int pageSize = 10) : IRequest<Pagination<ApproveRequestRelatedDTO>>;

    public class GetApproveHandler : IRequestHandler<GetApproveQuery, Pagination<ApproveRequestRelatedDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetApproveHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<ApproveRequestRelatedDTO>> Handle(GetApproveQuery request, CancellationToken cancellationToken)
        {
            var approveRequest = await _unitOfWork.ApproveRequestRepository.GetAsync(
                pageIndex: request.pageIndex, pageSize: request.pageSize);

            var result = _mapper.Map<Pagination<ApproveRequestRelatedDTO>>(approveRequest);

            return result;
        }
    }
}


