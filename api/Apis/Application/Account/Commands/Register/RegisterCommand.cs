﻿using Application.Common.Exceptions;
using Application.Emails.Commands.SendMailUserRegister;
using Application.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Account.Commands.Register;

public record RegisterCommand : IRequest<AccountDto>
{
    public string Email { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccountDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public RegisterCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<AccountDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _unitOfWork.UserRepository.CheckExistUser(request.Email);

        if (isExist)
            throw new NotFoundException("Email is exist !!!");

        var user = _mapper.Map<User>(request);
        user.Role = UserRoleType.Trainee;
        user.Status = UserStatus.Active;
        user.Password = user.Password.Hash();

        await _unitOfWork.ExecuteTransactionAsync(() =>
        {
            _unitOfWork.UserRepository.AddAsync(user);
        });
        var response = _mapper.Map<AccountDto>(user);
        await _mediator.Send(new SendMailUserRegisterCommand(response));
        return response;
    }
}
