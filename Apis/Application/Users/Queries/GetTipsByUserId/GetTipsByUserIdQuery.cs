using AutoMapper;
using MediatR;

namespace Application.Users.Queries.GetTipsByUserId
{
    public record GetTipsByUserIdQuery(int UserId) : IRequest<List<Tip>>;

    public class GetTipsByUserIdHandler : IRequestHandler<GetTipsByUserIdQuery, List<Tip>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTipsByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Tip>> Handle(GetTipsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var isShowTips = await _unitOfWork.UserRepository.AnyAsync(x => x.Id == request.UserId && x.IsShowTipCreatingClass == true);
            var result = isShowTips ? new List<Tip>(){
                new Tip(){ Step = 1, Category = "General", Message = "Fill information" },
                new Tip(){ Step = 2, Category = "Attendee", Message = "Fill attendee condition and number" },
                new Tip(){ Step = 3, Category = "TimeFrame", Message = "Pick started day" },
                new Tip(){ Step = 4, Category = "Next", Message = "Go to next step" },
            } : new List<Tip>();
            return result;
        }
    }
}

public class Tip
{
#pragma warning disable
    public int Step { get; init; }
    public string Category { get; init; }
    public string Message { get; init; }
}