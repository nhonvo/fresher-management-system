using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class TrainingProgramRepository :GenericRepository<TrainingProgram>, ITrainingProgramRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public TrainingProgramRepository(ApplicationDbContext dbContext, ICacheService cache) : base(dbContext, cache)
        {
            _dbcontext = dbContext;
        }
    }
}
