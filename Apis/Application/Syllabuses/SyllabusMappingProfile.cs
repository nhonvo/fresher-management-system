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
            // duplicate 
            CreateMap<Unit, SyllabusUnitDuplicate>().ReverseMap();
            CreateMap<Syllabus, SyllabusDuplicate>().ReverseMap();
            CreateMap<Lesson, SyllabusLessonDuplicate>().ReverseMap();
            CreateMap<TrainingMaterial, TrainingMaterialDuplicate>().ReverseMap();
            // related
            CreateMap<Unit, SyllabusUnitRelated>().ReverseMap();
            CreateMap<Lesson, SyllabusLessonRelated>().ReverseMap();
            CreateMap<TrainingMaterialRelated, TrainingMaterial>().ReverseMap();
            CreateMap<Syllabus, SyllabusRelated>().ReverseMap();
        }
    }
}