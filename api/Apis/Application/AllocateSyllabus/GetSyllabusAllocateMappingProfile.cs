using Application.Lessons.DTO;
using Application.Syllabuses.DTOs;
using Application.Units.DTOs;
using Application.ViewModels.OutputStandards;
using Application.ViewModels.Syllabus;
using Application.ViewModels.TrainingMaterials;
using Application.ViewModels.UnitLessons;
using Application.ViewModels.Units;
using AutoMapper;
using Domain.Entities;

namespace Application.AllocateSyllabus
{
    public class GetSyllabusAllocateMappingProfile : Profile
    {
        public GetSyllabusAllocateMappingProfile()
        {
            CreateMap<Unit, UnitDTO>().ReverseMap();
            CreateMap<Unit, UnitHasIdDTO>().ReverseMap();
            CreateMap<UnitDTO, UnitLessonDTO>().ReverseMap();
            CreateMap<UnitDTO, SyllabusDTO>().ReverseMap();
            CreateMap<Syllabus, UnitHasIdDTO>().ReverseMap();

            CreateMap<Unit, UnitLessonDTO>().ReverseMap();
            CreateMap<Unit, UnitLessonViewDTO>().ReverseMap();

            CreateMap<SyllabusViewDTO, Syllabus>().ReverseMap();

            CreateMap<Unit, UnitViewDTO>().ReverseMap();
            CreateMap<Lesson, UnitLessonViewDTO>().ReverseMap();

            CreateMap<TrainingMaterial, TrainingMaterialViewDTO>().ReverseMap();

            CreateMap<OutputStandard, OutputStandardViewDTO>().ReverseMap();
        }
    }
}
