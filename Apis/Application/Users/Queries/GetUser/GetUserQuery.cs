using Application.Commons;
using Application.Users.DTO;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Users.Queries.GetUser
{
    public record GetUserQuery(
        string? keyword = null,
        int PageIndex = 0,
        int PageSize = 10,
        SortType sortType = SortType.Ascending) : IRequest<Pagination<UserDTO>>;
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
            var user = await _unitOfWork.UserRepository.GetAsync<string>(
             pageIndex: request.PageIndex,
             pageSize: request.PageSize,
             sortType: SortType.Ascending,
             keySelectorForSort: x => x.Email);

            var result = _mapper.Map<Pagination<UserDTO>>(user);
            return result;
        }
    }
}