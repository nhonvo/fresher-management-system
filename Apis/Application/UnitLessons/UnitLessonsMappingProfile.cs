using Application.UnitLessons.Commands.CreateUnitLesson;
using Application.UnitLessons.Commands.UpdateUnitLesson;
using Application.UnitLessons.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.UnitLessons
{
    public class UnitLessonsMappingProfile : Profile
    {
        public UnitLessonsMappingProfile()
        {
            CreateMap<UnitLesson, UnitLessonDTO>().ReverseMap();
            CreateMap<UnitLesson, CreateUnitLessonCommand>().ReverseMap();
            CreateMap<UnitLesson, UpdateUnitLessonCommand>().ReverseMap();
        }
    }
}
