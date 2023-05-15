using Application.Commons;
using Application.Users.DTO;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<UserDTO>>;
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Pagination<UserDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.ToPagination(request.PageIndex, request.PageSize);
            var result = _mapper.Map<Pagination<UserDTO>>(user);
            return result;
        }
    }
}