using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories;

public class ClassStudentRepository : GenericRepository<ClassStudent>, IClassStudentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ClassStudentRepository(ApplicationDbContext context) : base(context)
    {
        _dbContext = context;
    }
}
