using Application.Class.DTO;
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
            CreateMap<ProgramSyllabusDuplicate, ProgramSyllabus>().ReverseMap();
            CreateMap<TrainingProgramSyllabusDuplicate, Syllabus>().ReverseMap();
            CreateMap<TrainingProgramTestAssessmentDuplicate, TestAssessment>().ReverseMap();
            CreateMap<TrainingProgramUnitDuplicate, Unit>().ReverseMap();
            CreateMap<TrainingProgramLessonDuplicate, UnitLesson>().ReverseMap();
            CreateMap<TrainingProgramTrainingMaterialDuplicate, TrainingMaterial>().ReverseMap();
        }
    }
}