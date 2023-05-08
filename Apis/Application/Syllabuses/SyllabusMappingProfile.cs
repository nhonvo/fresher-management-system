using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Entities.Syllabuses;

namespace Application.Syllabuses
{
    public class SyllabusMappingProfile : Profile
    {
        public SyllabusMappingProfile()
        {
            CreateMap<Syllabus, SyllabusDTO>().ReverseMap();
        }
    }
}