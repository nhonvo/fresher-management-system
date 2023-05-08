using AutoMapper;
using Application.Lectures.DTO;
using Domain.Entities;

namespace Application.Lectures
{
    public class LectureMappingProfile : Profile
    {
        public LectureMappingProfile() => CreateMap<Lecture, LectureDTO>().ReverseMap();
    }
}