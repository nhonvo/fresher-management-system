using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class SyllabusRepository : GenericRepository<Syllabus>, ISyllabusRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SyllabusRepository(ApplicationDbContext dbContext, ICacheService cache) : base(dbContext, cache)
        {
            _dbContext = dbContext;
        }
    }
}
