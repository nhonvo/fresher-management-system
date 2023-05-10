using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class ReportAttendanceRepository : GenericRepository<ReportAttendence>, IReportAttendanceRepository
    {
        public ReportAttendanceRepository(ApplicationDbContext context, ICacheService cacheService) : base(context, cacheService)
        {
        }
    }
}
