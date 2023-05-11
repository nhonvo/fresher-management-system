using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;
namespace Infrastructures.Repositories
{
    public class ClassRepository : GenericRepository<TrainingClass>, IClassRepository
    {
        public ClassRepository(ApplicationDbContext context, ICacheService cache) : base(context, cache)
        {
        }


    }
}
