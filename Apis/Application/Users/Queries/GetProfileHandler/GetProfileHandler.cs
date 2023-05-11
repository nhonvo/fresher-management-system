using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Users.DTO;
using AutoMapper;
using MediatR;

namespace Application.Users.GetProfile.Queries
{
    public record GetProfileQuery() : IRequest<UserDTO>;

    public class GetProfileHandler : IRequestHandler<GetProfileQuery, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;

        public GetProfileHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
        }
        public async Task<UserDTO> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(_claimService.CurrentUserId);
            var result = _mapper.Map<UserDTO>(user);
            return result ?? throw new NotFoundException("User not found", _claimService.CurrentUserId);

        }
    }
}