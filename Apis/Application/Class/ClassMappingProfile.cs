using Application.Class.Commands.CreateClass;
using Application.Class.Commands.UpdateClass;
using Application.Class.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Class
{
    public class ClassMappingProfile : Profile
    {
        public ClassMappingProfile()
        {
            CreateMap<TrainingClass, ClassDTO>().ReverseMap();
            CreateMap<TrainingClass, ClassProgram>().ReverseMap();
            CreateMap<TrainingClass, CreateClassCommand>().ReverseMap();
            CreateMap<TrainingClass, UpdateClassCommand>().ReverseMap();
            CreateMap<TrainingProgram, TrainingProgramss>().ReverseMap();
        }
    }
}