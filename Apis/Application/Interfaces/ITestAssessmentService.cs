using Application.ViewModels.TestAssessmentViewModels;

namespace Application.Interfaces
{
    public interface ITestAssessmentService
    {
        public Task<List<TestAssessmentViewModel>> GetTestAssessmentPagingsionAsync(int pageIndex = 0, int pageSize = 10);
        public Task<List<TestAssessmentViewModel>> GetChemicalAsync();

    }
}
