using Application.FeedBacks.DTO;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.FeedBacks.Commands.CreateFeedBack
{
    public record CreateFeedBackCommand : IRequest<FeedBackDTO>
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
    }

    public class CreateFeedBackHandler : IRequestHandler<CreateFeedBackCommand, FeedBackDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFeedBackHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FeedBackDTO> Handle(CreateFeedBackCommand request, CancellationToken cancellationToken)
        {
            //var feedback = _mapper.Map<FeedBack>(request);

            //await _unitOfWork.ExecuteTransactionAsync(() =>
            //{
            //    _unitOfWork.FeedBackRepository.AddAsync(feedback);
            //});
            //var result = _mapper.Map<FeedBackDTO>(feedback);

            //return result ?? throw new NotFoundException("Feed back can not create");

            try
            {
                var feedback = _mapper.Map<FeedBack>(request);

                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.FeedBackRepository.AddAsync(feedback);
                });

                var result = _mapper.Map<FeedBackDTO>(feedback);

                return result; //?? throw new NotFoundException("Feedback not found");
            }
            catch (Exception ex)
            {
                // Handle the exception here
                throw new Exception($"An error occurred while creating a feedback: {ex.Message}");
            }

        }
    }
}
