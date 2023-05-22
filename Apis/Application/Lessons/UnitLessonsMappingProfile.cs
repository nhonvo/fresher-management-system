using Application.Lessons.Commands.CreateUnitLesson;
using Application.Lessons.Commands.UpdateUnitLesson;
using Application.Lessons.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Lessons
{
    public class UnitLessonsMappingProfile : Profile
    {
        public UnitLessonsMappingProfile()
        {
            CreateMap<Lesson, UnitLessonDTO>().ReverseMap();
            CreateMap<Lesson, UnitLessonHasIdDTO>().ReverseMap();
            CreateMap<Lesson, CreateUnitLessonCommand>().ReverseMap();
            CreateMap<Lesson, UpdateUnitLessonCommand>().ReverseMap();
        }
    }
}
