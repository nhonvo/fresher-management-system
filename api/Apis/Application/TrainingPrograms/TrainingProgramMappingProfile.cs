using Application.TrainingPrograms.Commands.CreateTrainingProgram;
using Application.TrainingPrograms.Commands.UpdateTrainingProgram;
using Application.TrainingPrograms.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.TrainingPrograms
{
    public class TrainingProgramMappingProfile : Profile
    {
        public TrainingProgramMappingProfile()
        {
            CreateMap<TrainingProgram, TrainingProgramDTO>().ReverseMap();
            CreateMap<TrainingProgram, CreateTrainingProgramCommand>().ReverseMap();
            CreateMap<TrainingProgram, UpdateTrainingProgramCommand>().ReverseMap();
            // duplicate
            CreateMap<TrainingProgramDuplicate, TrainingProgram>().ReverseMap();
            CreateMap<TrainingProgramProgramSyllabusDuplicate, ProgramSyllabus>().ReverseMap();
            CreateMap<TrainingProgramProgramSyllabusDuplicate, ProgramSyllabus>().ReverseMap();
            CreateMap<TrainingProgramSyllabusDuplicate, Syllabus>().ReverseMap();
            CreateMap<TrainingProgramTestAssessmentDuplicate, TestAssessment>().ReverseMap();
            CreateMap<TrainingProgramUnitDuplicate, Unit>().ReverseMap();
            CreateMap<TrainingProgramLessonDuplicate, Lesson>().ReverseMap();
            CreateMap<TrainingProgramTrainingMaterialDuplicate, TrainingMaterial>().ReverseMap();
            // related
            CreateMap<TrainingProgram, TrainingProgramRelated>().ReverseMap();
            CreateMap<ProgramSyllabus, TrainingProgramProgramSyllabusRelated>().ReverseMap();
            CreateMap<Syllabus, TrainingProgramSyllabusRelated>().ReverseMap();
            CreateMap<TestAssessment, TrainingProgramTestAssessmentRelated>().ReverseMap();
            CreateMap<Unit, TrainingProgramUnitRelated>().ReverseMap();
            CreateMap<Lesson, TrainingProgramLessonRelated>().ReverseMap();
            CreateMap<TrainingMaterial, TrainingProgramTrainingMaterialRelated>().ReverseMap();
            // related has id
            CreateMap<TrainingProgram, TrainingProgramHasIdRelated>().ReverseMap();
            CreateMap<ProgramSyllabus, TrainingProgramProgramSyllabusHasIdRelated>().ReverseMap();
            CreateMap<Syllabus, TrainingProgramSyllabusHasIdRelated>().ReverseMap();
            CreateMap<TestAssessment, TrainingProgramTestAssessmentHasIdRelated>().ReverseMap();
            CreateMap<Unit, TrainingProgramUnitHasIdRelated>().ReverseMap();
            CreateMap<Lesson, TrainingProgramLessonHasIdRelated>().ReverseMap();
            CreateMap<TrainingMaterial, TrainingProgramTrainingMaterialHasIdRelated>().ReverseMap();

        }
    }
}