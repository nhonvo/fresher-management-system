using Application.Common.Exceptions;
using Application.Users.DTO;
using AutoMapper;
using MediatR;

namespace Application.Users.GetUserById.Queries
{
    public record GetUserByIdQuery(int id) : IRequest<UserDTO>;

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.id);
            var result = _mapper.Map<UserDTO>(user);
            return result ?? throw new NotFoundException("User not found", request.id);

        }
    }
}