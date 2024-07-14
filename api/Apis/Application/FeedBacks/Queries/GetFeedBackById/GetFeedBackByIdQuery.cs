using Application.Common.Exceptions;
using Application.FeedBacks.DTO;
using AutoMapper;
using MediatR;

namespace Application.FeedBacks.Queries.GetFeedBackById
{
    public record GetFeedBackByIdQuery(int id) : IRequest<FeedBackDTO>;
    public class GetFeedBackByIdHandler : IRequestHandler<GetFeedBackByIdQuery, FeedBackDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFeedBackByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FeedBackDTO> Handle(GetFeedBackByIdQuery request, CancellationToken cancellationToken)
        {
            var feedback = await _unitOfWork.FeedBackRepository.GetByIdAsync(request.id);
            var result = _mapper.Map<FeedBackDTO>(feedback);
            return result ?? throw new NotFoundException("Feedback not found", request.id);
        }
    }

}
