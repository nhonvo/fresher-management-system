using Application.Account.DTOs;
using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Account.Commands.Register;

public record RegisterCommand : IRequest<AccountDTO>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsMale { get; set; } = true;
}
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccountDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJWTService _jwtService;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IJWTService jwtService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task<AccountDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // avatarURL, RoleId
        var isExist = await _unitOfWork.UserRepository.CheckExistUser(request.Email);

        if (isExist)
            throw new NotFoundException("Email is exist !!!");

        var user = _mapper.Map<User>(request);
        //user.RoleId = 1;
        // user.AvatarURL = "null";
        user.Password = _jwtService.Hash(user.Password);
        try
        {
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.UserRepository.AddAsync(user);
            });
            var response = _mapper.Map<AccountDTO>(user);
            return response;
        }
        catch (Exception)
        {
            throw new NotFoundException("Register fail!!");
        }
    }
}
