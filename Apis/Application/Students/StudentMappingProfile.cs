using AutoMapper;
using Domain.Entities;
using Domain.Entities.Users;

namespace Application.Student
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            // CreateMap<User, StudentDTO>().ReverseMap();
            // CreateMap<User, CreateStudentCommand>().ReverseMap();
            // CreateMap<User, UpdateStudentCommand>().ReverseMap();
            // CreateMap<TrainingProgram, TrainingPrograms>().ReverseMap();
        }
    }
}