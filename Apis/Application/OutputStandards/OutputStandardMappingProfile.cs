using Application.OutputStandards.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.OutputStandards;

public class OutputStandardMappingProfile : Profile
{
    public OutputStandardMappingProfile()
    {
        CreateMap<OutputStandard, OutputStandardDTO>().ReverseMap();
    }
}
