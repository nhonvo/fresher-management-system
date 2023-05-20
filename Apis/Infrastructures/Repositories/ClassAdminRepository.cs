using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class ClassAdminRepository : GenericRepository<ClassAdmin>, IClassAdminRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public ClassAdminRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
    }
}
