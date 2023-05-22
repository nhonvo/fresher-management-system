using Application.Class.Commands.CreateClass;
using Application.Class.Commands.UpdateClass;
using Application.Class.DTOs;
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
            // duplicate
            CreateMap<TrainingProgram, TrainingProgramDuplicate>().ReverseMap();
            CreateMap<ProgramSyllabus, TrainingProgramProgramSyllabusDuplicate>().ReverseMap();
            CreateMap<Syllabus, TrainingProgramSyllabusDuplicate>().ReverseMap();
            CreateMap<Unit, TrainingProgramUnitDuplicate>().ReverseMap();
            CreateMap<TestAssessment, TrainingProgramTestAssessmentDuplicate>().ReverseMap();
            CreateMap<Lesson, TrainingProgramLessonDuplicate>().ReverseMap();
            CreateMap<TrainingMaterial, TrainingProgramTrainingMaterialDuplicate>().ReverseMap();
            // get class detail
            CreateMap<TrainingProgram, TrainingProgramss>().ReverseMap();
            CreateMap<TrainingProgram, TrainingProgramDetail>().ReverseMap();
            CreateMap<ProgramSyllabus, ProgramSyllabusDetail>().ReverseMap();
            CreateMap<SyllabusDetail, Syllabus>().ReverseMap();
            CreateMap<UnitDetail, Unit>().ReverseMap();
            CreateMap<LessonDetail, Lesson>().ReverseMap();
            CreateMap<TrainingMaterialDetail, TrainingMaterial>().ReverseMap();

            CreateMap<TrainingProgramDTO, TrainingProgram>().ReverseMap();
            CreateMap<TrainingClass, CreateClassCommand>().ReverseMap();
            CreateMap<TrainingClass, UpdateClassCommand>().ReverseMap();
            CreateMap<ClassAdmin, ClassClassAdminRelated>().ReverseMap();
            CreateMap<ClassRelated, TrainingClass>().ReverseMap();
            CreateMap<ClassDetail, TrainingClass>().ReverseMap();
        }
    }
}