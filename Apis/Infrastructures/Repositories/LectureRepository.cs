using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class LectureRepository : GenericRepository<Lecture>, ILectureRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public LectureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}