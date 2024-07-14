using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;

namespace Infrastructures.Repositories;

public class CalenderRepository : GenericRepository<Calender>, ICalenderRepository
{
    private readonly ApplicationDbContext _context;

    public CalenderRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
