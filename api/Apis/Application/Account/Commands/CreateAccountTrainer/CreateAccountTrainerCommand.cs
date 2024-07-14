﻿using Application.Account.DTOs;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Utils;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Account.Commands.CreateAccountTrainer;

public record CreateAccountTrainerCommand(string Email, string Password) : IRequest<AccountDTO>;

public class CreateAccountTrainerCommandHandler : IRequestHandler<CreateAccountTrainerCommand, AccountDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;

    public CreateAccountTrainerCommandHandler(
        IUnitOfWork unitOfWork,
        IClaimService claimService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _claimService = claimService;
        _mapper = mapper;
    }

    public async Task<AccountDTO> Handle(CreateAccountTrainerCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _unitOfWork.UserRepository.CheckExistUser(request.Email);

        if (isExist)
        {
            throw new NotFoundException("Email is exist !!!");
        }

        var user = _mapper.Map<User>(request);
        user.Role = Domain.Enums.UserRoleType.Trainer;
        user.Status = Domain.Enums.UserStatus.Active;
        user.Password = request.Password.Hash();
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            await _unitOfWork.UserRepository.AddAsync(user);
        });
        var result = _mapper.Map<AccountDTO>(user);
        return result;
    }
}
