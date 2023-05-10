
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
            // CreateMap<TrainingClass, ClassProgram>().ReverseMap();
            // CreateMap<TrainingClass, CreateClassCommand>().ReverseMap();
            // CreateMap<TrainingClass, UpdateClassCommand>().ReverseMap();
            // CreateMap<TrainingProgram, TrainingPrograms>().ReverseMap();
        }
    }
}