using Application.Calenders.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Calenders;

public class CalenderMappingProfile : Profile
{
    public CalenderMappingProfile()
    {
        CreateMap<Calender, CalenderDTO>().ReverseMap();
    }
}
