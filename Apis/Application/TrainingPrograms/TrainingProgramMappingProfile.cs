using Application.TrainingPrograms.Commands.CreateTrainingProgram;
using Application.TrainingPrograms.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.TrainingPrograms
{
    public class TrainingProgramMappingProfile : Profile
    {
        public TrainingProgramMappingProfile()
        {
            CreateMap<TrainingProgram, TrainingProgramDTO>();
            CreateMap<TrainingProgram, CreateTrainingProgramCommand>();

        }
    }
}