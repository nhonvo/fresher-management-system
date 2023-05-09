using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;
namespace Infrastructures.Repositories
{
    public class TestAssessmentRepository : GenericRepository<TestAssessment>, ITestAssessmentRepository
    {
        public TestAssessmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
