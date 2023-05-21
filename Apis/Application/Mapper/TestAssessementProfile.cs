using Application.TestAssessments.Commands.CreateTestAssessment;
using Application.TestAssessments.Commands.UpdateTestAssessment;
using Application.TestAssessments.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class TestAssessementProfile : Profile
    {
        public TestAssessementProfile()
        {
            CreateMap<TestAssessment, TestAssessmentDTO>().ReverseMap();
            CreateMap<CreateTestAssessmentCommand, TestAssessment>().ReverseMap();
            CreateMap<UpdateTestAssessmentCommand, TestAssessment>().ReverseMap();
        }
    }
}
