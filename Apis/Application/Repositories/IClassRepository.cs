using Application.ViewModels;
using Domain.Entities;

namespace Application.Repositories;

public interface IClassRepository : IGenericRepository<TrainingClass>
{
    Task<ClassDuration> GetClassDurationAsync(int classId);
}
