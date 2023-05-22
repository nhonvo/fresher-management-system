using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public LessonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
    }
}
