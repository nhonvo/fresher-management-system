using Application.Units.Commands.CreateUnit;
using Application.Units.Commands.UpdateUnit;
using Application.Units.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Units
{
    public class UnitsMappingProfile : Profile
    {
        public UnitsMappingProfile()
        {
            CreateMap<Unit, UnitDTO>().ReverseMap();
            CreateMap<Unit, UnitHasIdDTO>().ReverseMap();
            CreateMap<Unit, CreateUnitCommand>().ReverseMap();
            CreateMap<Unit, UpdateUnitCommand>().ReverseMap();
        }
    }
}
