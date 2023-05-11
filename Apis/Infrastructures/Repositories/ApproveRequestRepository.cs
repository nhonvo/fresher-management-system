using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class ApproveRequestRepository : GenericRepository<ApproveRequest>, IApproveRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ApproveRequestRepository(ApplicationDbContext dbContext, ICacheService cache) : base(dbContext, cache)
        {
            _dbContext = dbContext;
        }
    }
}
