using AutoMapper;
using Domain.Entities;

namespace Application.Class.Commands.SetCalenders;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CalenderCreateDTO, Calender>();
    }
}
