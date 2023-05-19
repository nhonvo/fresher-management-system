using Application.Class.Commands.CreateClass;
using Application.Class.Commands.UpdateClass;
using Application.Class.DTO;
using Application.Class.DTOs;
using Application.TrainingPrograms.DTOs;
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
            CreateMap<TrainingProgram, TrainingProgramDuplicate>().ReverseMap();
            CreateMap<ProgramSyllabus, ProgramSyllabusDuplicate>().ReverseMap();
            CreateMap<TrainingProgram, TrainingProgramss>().ReverseMap();
            // get class detail
            CreateMap<TrainingProgram, TrainingProgramDetail>().ReverseMap();
            CreateMap<ProgramSyllabus, ProgramSyllabusDetail>().ReverseMap();
            CreateMap<SyllabusDetail, Syllabus>().ReverseMap();
            CreateMap<UnitDetail, Unit>().ReverseMap();
            CreateMap<LessonDetail, UnitLesson>().ReverseMap();
            CreateMap<TrainingMaterialDetail, TrainingMaterial>().ReverseMap();
            
            CreateMap<TrainingProgramDTO, TrainingProgram>().ReverseMap();
            CreateMap<TrainingClass, CreateClassCommand>().ReverseMap();
            CreateMap<TrainingClass, UpdateClassCommand>().ReverseMap();
            CreateMap<ClassAdmin, Admin>().ReverseMap();
            CreateMap<AdminClass, TrainingClass>().ReverseMap();
            CreateMap<ClassDetail, TrainingClass>().ReverseMap();
        }
    }
}