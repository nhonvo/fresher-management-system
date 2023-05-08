using Application.Repositories;
using Domain.Entities.Syllabuses;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class SyllabusRepository : GenericRepository<Syllabus>, ISyllabusRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SyllabusRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
