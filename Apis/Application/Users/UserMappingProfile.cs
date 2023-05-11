using Application.Users.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}