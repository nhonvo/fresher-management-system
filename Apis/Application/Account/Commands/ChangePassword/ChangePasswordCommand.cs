using Application.Account.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Account.Commands.ChangPassword;

public record ChangePasswordCommand(string Password, string NewPassword) : IRequest<AccountDTO>;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, AccountDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimService _claimService;
    private readonly IJWTService _jwtService;
    private readonly IMapper _mapper;

    public ChangePasswordCommandHandler(
        IUnitOfWork unitOfWork,
        IClaimService claimService,
        IJWTService jwtService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _claimService = claimService;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    public async Task<AccountDTO> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {

        var isUser = await _unitOfWork.UserRepository.GetByIdAsyncAsNoTracking(_claimService.CurrentUserId);
        isUser.Password = _jwtService.Hash(request.NewPassword);
        await _unitOfWork.ExecuteTransactionAsync(() =>
        {
            _unitOfWork.UserRepository.Update(isUser);
        });
        var result = _mapper.Map<AccountDTO>(isUser);
        return result;
    }
}
