using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;
namespace Infrastructures.Repositories
{
    public class ClassRepository : GenericRepository<TrainingClass>, IClassRepository
    {
        public ClassRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
