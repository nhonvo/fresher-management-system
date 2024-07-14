using Application.Account.DTOs;
using Application.Interfaces;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.Account.Commands.ChangPassword;

public record ChangePasswordCommand(string Password, string NewPassword) : IRequest<AccountDTO>;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, AccountDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;

    public ChangePasswordCommandHandler(
        IUnitOfWork unitOfWork,
        IClaimService claimService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _claimService = claimService;
        _mapper = mapper;
    }

    public async Task<AccountDTO> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {

        var isUser = await _unitOfWork.UserRepository.GetByIdAsyncAsNoTracking(_claimService.CurrentUserId);
        isUser.Password = request.NewPassword.Hash();
        await _unitOfWork.ExecuteTransactionAsync(() =>
        {
            _unitOfWork.UserRepository.Update(isUser);
        });
        var result = _mapper.Map<AccountDTO>(isUser);
        return result;
    }
}
