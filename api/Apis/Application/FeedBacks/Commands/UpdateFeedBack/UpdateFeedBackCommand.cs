using Application.Common.Exceptions;
using Application.FeedBacks.DTO;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.FeedBacks.Commands.UpdateFeedBack
{
    public record UpdateFeedBackCommand : IRequest<FeedBackDTO>
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
    }

    public class UpdateFeedBackHandler : IRequestHandler<UpdateFeedBackCommand, FeedBackDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateFeedBackHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FeedBackDTO> Handle(UpdateFeedBackCommand request, CancellationToken cancellationToken)
        {
            var feedback = await _unitOfWork.FeedBackRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (feedback == null)
                throw new NotFoundException("Feedback not found");
            feedback = _mapper.Map<FeedBack>(request);
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.FeedBackRepository.Update(feedback);
                await _unitOfWork.CommitAsync();
                var result = _mapper.Map<FeedBackDTO>(feedback);
                return result;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new NotFoundException("Update has some error");
            }
        }
    }
}
