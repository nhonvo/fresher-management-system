using Application.Commons;
using Application.FeedBacks.DTO;
using AutoMapper;
using MediatR;

namespace Application.FeedBacks.Queries.GetFeedBacks
{
    public record GetFeedBackQuery(int pageIndex = 0, int pageSize = 10) : IRequest<Pagination<FeedBackDTO>>;
    public class GetFeedBackHandler : IRequestHandler<GetFeedBackQuery, Pagination<FeedBackDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFeedBackHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<FeedBackDTO>> Handle(GetFeedBackQuery request, CancellationToken cancellationToken)
        {
            var feedback = await _unitOfWork.FeedBackRepository.GetAsync(pageIndex: request.pageIndex, pageSize: request.pageSize);
            var result = _mapper.Map<Pagination<FeedBackDTO>>(feedback);
            return result;
        }
    }
}
