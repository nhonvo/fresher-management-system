using Application.Account.DTOs;
using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Account.Commands.CreateAccount;

public record CreateAccountCommand(string Email, string Password) : IRequest<AccountDTO>;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimService _claimService;
    private readonly IJWTService _jwtService;
    private readonly IMapper _mapper;

    public CreateAccountCommandHandler(
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

    public async Task<AccountDTO> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _unitOfWork.UserRepository.CheckExistUser(request.Email);

        if (isExist)
            throw new NotFoundException("Email is exist !!!");

        var user = _mapper.Map<User>(request);
        user.Role = Domain.Enums.UserRoleType.ClassAdmin;
        user.Status = Domain.Enums.UserStatus.Active;
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            await _unitOfWork.UserRepository.AddAsync(user);
        });
        var result = _mapper.Map<AccountDTO>(user);
        return result;
    }
}
