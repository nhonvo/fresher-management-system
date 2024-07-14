using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class ClassTrainerRepository : GenericRepository<ClassTrainer>, IClassTrainerRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public ClassTrainerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
    }
}
