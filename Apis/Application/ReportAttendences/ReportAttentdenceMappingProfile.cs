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
            CreateMap<ReportAttendance, ReportAttendanceDTO>().ReverseMap();
            CreateMap<ReportAttendance, CreateReportAttendancesCommand>().ReverseMap();
            CreateMap<ReportAttendance, UpdateReportAttendancesCommand>().ReverseMap();


        }
    }
}
