using Application.TrainingMaterials.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.TrainingMaterials;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TrainingMaterial, TrainingMaterialDTO>();
    }
}
