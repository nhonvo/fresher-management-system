using Application.Account.DTOs;
using Application.Common.Exceptions;
using Application.Emails.Commands.SendMail;
using Application.Interfaces;
using Application.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Account.Commands.Register;

public record RegisterCommand : IRequest<AccountDTO>
{
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
}
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccountDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public RegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<AccountDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // avatarURL, RoleId
        var isExist = await _unitOfWork.UserRepository.CheckExistUser(request.Email);

        if (isExist)
            throw new NotFoundException("Email is exist !!!");

        var user = _mapper.Map<User>(request);
        user.Role = UserRoleType.Trainee;
        user.Status = UserStatus.Active;
        user.Password = user.Password.Hash();
        try
        {
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.UserRepository.AddAsync(user);
            });
            var response = _mapper.Map<AccountDTO>(user);
            await _mediator.Send(new SendMailCreateUserCommand(response));
            return response;
        }
        catch (Exception)
        {
            throw new NotFoundException("Register fail!!");
        }
    }
}
