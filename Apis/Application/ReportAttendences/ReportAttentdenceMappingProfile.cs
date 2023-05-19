using Application.ReportAttendances.Commands.CreateReportAttendances;
using Application.ReportAttendances.Commands.UpdateReportAttendances;
using Application.ReportAttendances.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.ReportAttendances
{
    public class ReportAttendanceMappingProfile : Profile
    {
        public ReportAttendanceMappingProfile()
        {
            CreateMap<Attendance, AttendanceDTO>().ReverseMap();
            CreateMap<Attendance, CreateReportAttendancesCommand>().ReverseMap();
            CreateMap<Attendance, UpdateReportAttendancesCommand>().ReverseMap();


        }
    }
}
