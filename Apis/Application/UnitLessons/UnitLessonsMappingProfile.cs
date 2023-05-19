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
            CreateMap<Lesson, UnitLessonDTO>().ReverseMap();
            CreateMap<Lesson, CreateUnitLessonCommand>().ReverseMap();
            CreateMap<Lesson, UpdateUnitLessonCommand>().ReverseMap();
        }
    }
}
