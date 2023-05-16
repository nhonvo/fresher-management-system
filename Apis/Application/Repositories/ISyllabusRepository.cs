using Domain.Entities;

namespace Application.Repositories
{
    public interface ISyllabusRepository : IGenericRepository<Syllabus>
    {
        Task<Syllabus> GetByIdAsync(object id);
    }
}
