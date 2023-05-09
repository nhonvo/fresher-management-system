using Application.TestAssessments.Commands.CreateTestAssessment;
using Application.TestAssessments.Commands.UpdateTestAssessment;
using Application.TestAssessments.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.TestAssessments;

public class TestAssessmentMappingProfile : Profile
{
    public TestAssessmentMappingProfile()
    {
        CreateMap<TestAssessment, TestAssessmentDTO>().ReverseMap();
        CreateMap<CreateTestAssessmentCommand, TestAssessment>().ReverseMap();
        CreateMap<UpdateTestAssessmentCommand, TestAssessment>().ReverseMap();
    }
}
