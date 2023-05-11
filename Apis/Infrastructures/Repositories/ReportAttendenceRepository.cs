using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class ReportAttendanceRepository : GenericRepository<ReportAttendance>, IReportAttendanceRepository
    {
        public ReportAttendanceRepository(ApplicationDbContext context, ICacheService cacheService) : base(context, cacheService)
        {
        }
    }
}
