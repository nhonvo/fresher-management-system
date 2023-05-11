using Application.Syllabuses.Commands.CreateSyllabus;
using Application.Syllabuses.Commands.UpdateSyllabus;
using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Syllabuses
{
    public class ClassMappingProfile : Profile
    {
        public ClassMappingProfile()
        {
            CreateMap<Syllabus, SyllabusDTO>().ReverseMap();
            CreateMap<Syllabus, CreateSyllabusCommand>().ReverseMap();
            CreateMap<Syllabus, UpdateSyllabusCommand>().ReverseMap();
            CreateMap<Unit, SyllabusUnit>().ReverseMap();
            CreateMap<UnitLesson, LessonUnit>().ReverseMap();
            CreateMap<LessonTrainingMaterial, TrainingMaterial>().ReverseMap();
        }
    }
}