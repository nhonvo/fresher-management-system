using Application.Commons;
using Application.ViewModels.TestAssessmentViewModels;

namespace Application.Interfaces
{
    public interface ITestAssessmentService
    {
        public Task<Pagination<TestAssessmentViewModel>> GetTestAssessmentPagingsionAsync(int pageIndex = 0, int pageSize = 10);
        public Task<List<TestAssessmentViewModel>> GetTestAssessmentAsync();
        public Task<TestAssessmentViewModel?> CreateTestAssessmentAsync(CreateTestAssessmentViewModel testAssessmentViewModel);
        public Task RemoveAsync(int id);
        public Task<TestAssessmentViewModel> UpdateAsync(int id, UpdateTestAssessmentViewModel updateDTO);
        public Task<Pagination<GetListSyllabusSocreOfStudentViewModel>> GetListSyllabusScoreOfStudentAsync(int id, int? classId, int pageIndex = 0, int pageSize = 10);
        public Task<Pagination<GetListSyllabusScoreOfClassViewModel>> GetListSyllabusScoreOfClassAsync(int id, int? studentId, int pageIndex = 0, int pageSize = 10);
        public Task<Pagination<GetClassGPAScoreOfStudentViewModel>> GetClassGPAScoreOfStudentAsync(int id, int classId, int pageIndex = 0, int pageSize = 10);
        public Task<Pagination<GetStudentGPAScoreOfClassViewModel>> GetStudentGPAScoreOfClassAsync(int id, int studentId, int pageIndex = 0, int pageSize = 10);

    }
}
