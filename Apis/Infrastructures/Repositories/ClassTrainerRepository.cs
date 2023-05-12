using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class ClassTrainerRepository :GenericRepository<ClassTrainer>, IClassTrainerRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public ClassTrainerRepository(ApplicationDbContext dbContext, ICacheService cache) : base(dbContext, cache)
        {
            _dbcontext = dbContext;
        }
    }
}
