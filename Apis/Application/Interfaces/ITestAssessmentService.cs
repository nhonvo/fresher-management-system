using Application.Commons;
using Application.ViewModels.TestAssessmentViewModels;

namespace Application.Interfaces
{
    public interface ITestAssessmentService
    {
        public Task<Pagination<TestAssessmentViewModel>> GetTestAssessmentPagingsionAsync(int pageIndex = 0, int pageSize = 10);
        public Task<List<TestAssessmentViewModel>> GetTestAssessmentAsync();
        public Task<TestAssessmentViewModel?> CreateTestAssessmentAsync(CreateTestAssessmentViewModel testAssessmentViewModel);
        Task RemoveAsync(int id);
        Task<TestAssessmentViewModel> UpdateAsync(int id, UpdateTestAssessmentViewModel updateDTO);
        Task<Pagination<GetStudentFinalSyllabusScoreViewModel>> GetStudentFinalSyllabusScoreAsync(int id, int pageIndex= 0, int pageSize = 10);
        Task<Pagination<GetClassFinalSyllabusScoreViewModel>> GetClassFinalSyllabusScoreAsync(int id, int pageIndex = 0, int pageSize = 10);

    }
}
