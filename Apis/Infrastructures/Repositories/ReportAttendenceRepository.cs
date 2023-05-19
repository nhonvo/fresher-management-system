using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class ReportAttendanceRepository : GenericRepository<Attendance>, IReportAttendanceRepository
    {
        public ReportAttendanceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
