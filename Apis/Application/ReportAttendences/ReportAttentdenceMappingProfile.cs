using Application.ReportAttendences.Commands.CreateReportAttendences;
using Application.ReportAttendences.Commands.UpdateReportAttendences;
using Application.ReportAttendences.DTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
