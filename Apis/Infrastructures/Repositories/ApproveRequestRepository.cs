using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class ApproveRequestRepository : GenericRepository<ApproveRequest>, IApproveRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ApproveRequestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
