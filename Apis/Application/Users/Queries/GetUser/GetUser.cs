using Application.Commons;
using Application.Users.DTO;
using AutoMapper;
using MediatR;

namespace Application.Users.GetUser.Queries
{
    public record GetUserQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<UserDTO>>;
    public class GetUserHandler : IRequestHandler<GetUserQuery, Pagination<UserDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<UserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.ToPagination(request.PageIndex, request.PageSize);
            var result = _mapper.Map<Pagination<UserDTO>>(user);
            return result;
        }
    }
}