using Application.Commons;
using Application.FeedBacks.DTO;
using AutoMapper;
using Domain.Aggregate.AppResult;
using MediatR;

namespace Application.FeedBacks.Queries.GetFeedBacks
{
    public record GetFeedBackQuery(int pageIndex = 0, int pageSize = 10) : IRequest<ApiResult<Pagination<FeedBackDTO>>>;
    public class GetFeedBackHandler : IRequestHandler<GetFeedBackQuery, ApiResult<Pagination<FeedBackDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFeedBackHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult<Pagination<FeedBackDTO>>> Handle(GetFeedBackQuery request, CancellationToken cancellationToken)
        {
            var feedback = await _unitOfWork.FeedBackRepository.ToPagination(request.pageIndex, request.pageSize);
            var result = _mapper.Map<Pagination<FeedBackDTO>>(feedback);
            return new ApiSuccessResult<Pagination<FeedBackDTO>>(result);
        }
    }
}
