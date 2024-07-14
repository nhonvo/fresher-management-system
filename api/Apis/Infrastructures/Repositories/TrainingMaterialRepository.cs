using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class TrainingMaterialRepository : GenericRepository<TrainingMaterial>, ITrainingMaterialRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public TrainingMaterialRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
    }
}
