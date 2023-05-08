using Application.Repositories;
using Domain.Entities.Users;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> CheckUserNameExited(string username) => _dbContext.Users.AnyAsync(u => u.FullName == username);

        public async Task<User> GetUserByUserNameAndPasswordHash(string username, string passwordHash)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(record => record.FullName == username
                                        && record.Password == passwordHash);
            if (user is null)
            {
                throw new Exception("UserName & password is not correct");
            }


            return user;
        }
    }
}
