using Application.Class.DTO;
using Application.Student.Commands.UpdateStudent;
using Application.Students.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Student
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<User, StudentDTO>().ReverseMap();
            CreateMap<User, UpdateStudentCommand>().ReverseMap();
        }
    }
}