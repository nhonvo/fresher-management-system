using Application.ViewModels.TestAssessmentViewModels;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class TestAssessementProfile : Profile
    {
        public TestAssessementProfile()
        {
            CreateMap<TestAssessmentViewModel, TestAssessment>().ReverseMap();
            CreateMap<CreateTestAssessmentViewModel, TestAssessment>().ReverseMap();
            CreateMap<UpdateTestAssessmentViewModel, TestAssessment>().ReverseMap();

        }
    }
}
