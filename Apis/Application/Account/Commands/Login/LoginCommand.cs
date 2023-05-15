using Application.Account.DTOs;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Utils;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Account.Commands.Login;

public record LoginCommand : IRequest<AccountDTO>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
public class LoginCommandHandler : IRequestHandler<LoginCommand, AccountDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;
    private readonly AppConfiguration _configuration;
    private readonly ICurrentTime _currentTime;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, AppConfiguration configuration, ICurrentTime currentTime)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
        _currentTime = currentTime;
    }
    public async Task<AccountDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.Find(request.Email);
        if (user == null)
            throw new NotFoundException("Incorrect email!!!");
        // verify passwordHash
        if (!TokenUtils.Verify(request.Password, user.Password))
            throw new NotFoundException("Incorrect password!!!");

        var newUser = _mapper.Map<AccountDTO>(user);

        newUser.Token = user.GenerateToken(_currentTime.GetCurrentTime(),
                                           _configuration.Jwt.Issuer,
                                           _configuration.Jwt.Audience,
                                           _configuration.Jwt.Key);
        newUser.ExpireDay = _currentTime.GetCurrentTime().AddDays(1);
        return newUser;
    }
}
