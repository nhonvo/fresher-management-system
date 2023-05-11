using Application.ViewModels.TestAssessmentViewModels;
using Domain.Entities;

namespace Application.Repositories;

public interface ITestAssessmentRepository : IGenericRepository<TestAssessment>
{
    Task<List<GetStudentTestScoreViewModel>> GetFinalScoreAsync(int id);
}
