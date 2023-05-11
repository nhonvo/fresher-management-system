using Application.Account.Commands.CreateAccount;
using Application.Account.Commands.Login;
using Application.Account.Commands.Register;
using Application.Account.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Account;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<AccountDTO, User>().ReverseMap();
        CreateMap<RegisterCommand, User>().ReverseMap();
        CreateMap<LoginCommand, User>().ReverseMap();
        CreateMap<CreateAccountCommand, User>().ReverseMap();
    }
}
