using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class FeedBackRepository : GenericRepository<FeedBack>, IFeedBackrepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public FeedBackRepository(ApplicationDbContext dbContext, ICacheService cacheService) : base(dbContext, cacheService)
        {
            _dbcontext = dbContext;
        }
    }
}
