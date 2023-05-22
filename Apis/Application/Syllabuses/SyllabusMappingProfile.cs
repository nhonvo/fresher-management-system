using Application.Syllabuses.Commands.AddOneLessonToUnit;
using Application.Syllabuses.Commands.AddOneMaterialToLesson;
using Application.Syllabuses.Commands.AddOneUnitToSyllabus;
using Application.Syllabuses.Commands.CreateSyllabus;
using Application.Syllabuses.Commands.UpdateSyllabus;
using Application.Syllabuses.DTOs;
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
            // add to syllabus
            CreateMap<Unit, AddOneUnitToSyllabusCommand>().ReverseMap();
            CreateMap<Lesson, AddOneLessonToUnitCommand>().ReverseMap();
            CreateMap<TrainingMaterial, AddOneMaterialToLessonCommand>().ReverseMap();
            // response
            CreateMap<Lesson, AddOneLessonToUnitResponse>().ReverseMap();
            CreateMap<TrainingMaterial, AddOneMaterialToLessonResponse>().ReverseMap();
            CreateMap<Unit, AddOneUnitToSyllabusResponse>().ReverseMap();
        }
    }
}