using Application.Account.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Account.Commands.AddRole;

public record AddRoleCommand(UserRoleType role, int userId) : IRequest<AccountDTO>;

public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, AccountDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;

    public AddRoleCommandHandler(
        IUnitOfWork unitOfWork,
        IClaimService claimService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _claimService = claimService;
        _mapper = mapper;
    }

    public async Task<AccountDTO> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsyncAsNoTracking(request.userId);
        user.Role = request.role;
        await _unitOfWork.ExecuteTransactionAsync(() =>
        {
            _unitOfWork.UserRepository.Update(user);
        });
        var result = _mapper.Map<AccountDTO>(user);
        return result;
    }
}
