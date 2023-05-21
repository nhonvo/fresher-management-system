using AutoMapper;
using Domain.Entities;

namespace Application.TrainingPrograms.Queries.GetPagedSyllabusesByTraningProgramId;

public class SyllabusMappingProfile : Profile
{
    public SyllabusMappingProfile()
    {
        CreateMap<Syllabus, SyllabusDTO>();
    }
}
