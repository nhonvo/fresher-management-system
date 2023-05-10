using Application.Account.DTOs;
using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
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
    private readonly IJWTService _jwtService;
    private readonly IMapper _mapper;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IJWTService jwtService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _mapper = mapper;
    }
    public async Task<AccountDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.Find(request.Email);
        if (user == null)
            throw new NotFoundException("Incorrect email!!!");
        // verify passwordHash
        if (!_jwtService.Verify(request.Password, user.Password))
            throw new NotFoundException("Incorrect password!!!");

        var newUser = _mapper.Map<AccountDTO>(user);

        newUser.Token = _jwtService.GenerateJWT(user);

        newUser.ExpireDay = DateTime.Now.AddDays(1);

        return newUser;
    }
}
