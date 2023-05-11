using Application.Commons;
using Application.FeedBacks.DTO;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.FeedBacks.Queries.GetFeedbackByTrainee
{
    public record GetFeedBackByTrainerQuery(string? name, int pageIndex = 0, int pageSize = 10) : IRequest<Pagination<FeedBackDTO>>;
    public class GetFeedBackTrainerHandler : IRequestHandler<GetFeedBackByTrainerQuery, Pagination<FeedBackDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetFeedBackTrainerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<FeedBackDTO>> Handle(GetFeedBackByTrainerQuery request, CancellationToken cancellationToken)
        {
            var feedback = await _unitOfWork.FeedBackRepository.GetAsync(
                filter: x=>x.Trainer.Name.Contains(request.name) && x.Trainer.Role == Domain.Enums.UserRoleType.Trainer,
                include:x=>x.Include(x=>x.Trainer),
                pageIndex: request.pageIndex,
                pageSize: request.pageSize);
            var result = _mapper.Map<Pagination<FeedBackDTO>>(feedback);
            return result;
        }
    }
}
