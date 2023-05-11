using Application.ReportAttendences.Commands.CreateReportAttendences;
using Application.ReportAttendences.Commands.UpdateReportAttendences;
using Application.ReportAttendences.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.ReportAttendences
{
    public class ReportAttentdenceMappingProfile : Profile
    {
        public ReportAttentdenceMappingProfile()
        {
            CreateMap<ReportAttendence, ReportAttendenceDTO>().ReverseMap();
            CreateMap<ReportAttendence, CreateReportAttendencesCommand>().ReverseMap();
            CreateMap<ReportAttendence, UpdateReportAttendencesCommand>().ReverseMap();


        }
    }
}
