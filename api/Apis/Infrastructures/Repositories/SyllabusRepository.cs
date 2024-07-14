using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class SyllabusRepository : GenericRepository<Syllabus>, ISyllabusRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SyllabusRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Syllabus> GetByIdAsync(int id)
        {
            IQueryable<Syllabus> query = _dbSet;
            var syllabus = await _dbSet.Where(s => s.Id == id).Include(s => s.Units).ThenInclude(u => u.SyllabusSession).FirstOrDefaultAsync();
            if (syllabus is null)
            {
                throw new Exception("Syllabus not found");
            }
            return syllabus;
        }
        public async Task<Syllabus?> GetSyllabusRelationUnitAsync(int id) => await _dbSet
                                                                            .Include(s => s.Units)
                                                                                .ThenInclude(u => u.Lessons)
                                                                                .ThenInclude(ul => ul.OutputStandard)
                                                                            .Include(s => s.Units)
                                                                            .ThenInclude(u => u.Lessons)
                                                                            .ThenInclude(ul => ul.TrainingMaterials)
                                                                            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
