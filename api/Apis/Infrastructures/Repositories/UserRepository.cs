using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> CheckExistUser(string email)
             => await _context.Users.AnyAsync(x => x.Email == email);

        public async Task<User> Find(string email)
           => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}
