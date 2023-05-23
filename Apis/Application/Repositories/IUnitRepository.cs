using Domain.Entities;

namespace Application.Repositories
{
    public interface IUnitRepository : IGenericRepository<Unit>
    {
        Task<Unit> GetUnitWithLesson(int id);
    }
}
