using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AttendanceRepository(ApplicationDbContext dbContext, ICacheService cache) : base(dbContext, cache)
        {
            _dbContext = dbContext;
        }
    }
}