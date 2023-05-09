using Application.TestAssessments.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.TestAssessments;

public class TestAssessmentMappingProfile : Profile
{
    public TestAssessmentMappingProfile()
    {
        CreateMap<TestAssessment, TestAssessmentDTO>().ReverseMap();
    }
}
