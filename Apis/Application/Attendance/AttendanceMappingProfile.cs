using Application.Account.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Attendances;

public class AttendanceMappingProfile : Profile
{
    public AttendanceMappingProfile()
    {
        CreateMap<AttendanceDTO, Attendance>().ReverseMap();
        CreateMap<TrainingClasses, TrainingClass>().ReverseMap();
    }
}
