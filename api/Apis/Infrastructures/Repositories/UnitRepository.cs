using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public UnitRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<Unit?> GetUnitWithLesson(int id)
        {
            return await _dbcontext.Units.Where(x => x.Id == id).Include(x => x.Lessons).SingleOrDefaultAsync();
        }
    }
}
