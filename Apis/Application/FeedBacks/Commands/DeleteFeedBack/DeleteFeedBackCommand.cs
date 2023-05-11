using Application.Common.Exceptions;
using Application.FeedBacks.DTO;
using AutoMapper;
using MediatR;

namespace Application.FeedBacks.Commands.DeleteFeedBack
{
    public record DeleteFeedBackCommand(int id) : IRequest<FeedBackDTO>;
    public class DeleteFeedBackHandler : IRequestHandler<DeleteFeedBackCommand, FeedBackDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteFeedBackHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FeedBackDTO> Handle(DeleteFeedBackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _unitOfWork.FeedBackRepository.GetByIdAsync(request.id);
            if (feedback == null)
                throw new NotFoundException("Feedback not found");
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.FeedBackRepository.Delete(feedback);
                });
                var result = _mapper.Map<FeedBackDTO>(feedback);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
