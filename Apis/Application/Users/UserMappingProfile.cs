using Application.Student.Commands.UpdateUser;
using Application.Users.DTO;
using Application.Users.Queries.ExportUsers;
using AutoMapper;
using Domain.Entities;

namespace Application.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCSV>().ReverseMap();
            CreateMap<User, UserContainIdDTO>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();

        }
    }
}