using Application.Units.Commands.CreateUnit;
using Application.Units.Commands.UpdateUnit;
using Application.Units.DTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Units
{
    public class UnitsMappingProfile : Profile
    {
        public UnitsMappingProfile()
        {
            CreateMap<Unit, UnitDTO>().ReverseMap();
            CreateMap<Unit, CreateUnitCommand>().ReverseMap();
            CreateMap<Unit, UpdateUnitCommand>().ReverseMap();
        }
    }
}
