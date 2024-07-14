using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class ProgramSyllabusRepository : GenericRepository<ProgramSyllabus>, IProgramSyllabusRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public ProgramSyllabusRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbcontext = dbContext;
        }
    }
}
