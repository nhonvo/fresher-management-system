using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class UnitLessonRepository : GenericRepository<UnitLesson>, IUnitLessonRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public UnitLessonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
    }
}
