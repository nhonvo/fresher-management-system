using Application.ReportAttendances.Commands.CreateReportAttendances;
using Application.ReportAttendences.Commands.UpdateReportAttendences;
using Application.ReportAttendences.DTO;
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
