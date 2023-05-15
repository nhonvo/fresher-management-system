using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories
{
    public class FeedBackRepository : GenericRepository<FeedBack>, IFeedBackrepository
    {
        private readonly ApplicationDbContext _context;

        public FeedBackRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
