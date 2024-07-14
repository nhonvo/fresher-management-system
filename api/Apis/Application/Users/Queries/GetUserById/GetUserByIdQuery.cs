using Application.Commons;
using Application.Users.DTO;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<UserContainIdDTO>>;
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Pagination<UserContainIdDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<UserContainIdDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(pageIndex: request.PageIndex, pageSize: request.PageSize);
            var result = _mapper.Map<Pagination<UserContainIdDTO>>(user);
            return result;
        }
    }
}