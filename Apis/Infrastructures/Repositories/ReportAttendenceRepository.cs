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
    public class ReportAttendenceRepository : GenericRepository<ReportAttendence>, IReportAttendenceRepository
    {
        public ReportAttendenceRepository(ApplicationDbContext context, ICacheService cacheService) : base(context, cacheService)
        {
        }
    }
}
