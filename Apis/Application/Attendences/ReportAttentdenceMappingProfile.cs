using Application.Attendances.Commands.CreateAttendances;
using Application.Attendances.Commands.UpdateAttendances;
using Application.Attendances.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Attendances
{
    public class AttendanceMappingProfile : Profile
    {
        public AttendanceMappingProfile()
        {
            CreateMap<Attendance, AttendanceDTO>().ReverseMap();
            CreateMap<Attendance, CreateAttendancesCommand>().ReverseMap();
            CreateMap<Attendance, UpdateAttendancesCommand>().ReverseMap();


        }
    }
}
