using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class OutputStandardRepository : GenericRepository<OutputStandard>, IOutputStandardRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OutputStandardRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
