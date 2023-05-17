using Application.Repositories;
using Domain.Entities;
using IdentityModel;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;

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
            if(syllabus is null)
            {
                throw new Exception("Syllabus not found");
            }
            return syllabus;
        }
    }
}
