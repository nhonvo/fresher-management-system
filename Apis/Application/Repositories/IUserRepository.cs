using Domain.Entities;

namespace Application.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Finds a user by email. This is an alias for Find ( string )
        /// </summary>
        /// <param name="email">The email to search</param>
        Task<User> Find(string email);
        /// <summary>
        /// Checks if user exists. This is a check to make sure the user doesn't exist in the database
        /// </summary>
        /// <param name="email">E - mail of</param>
        Task<bool> CheckExistUser(string email);
    }
}
