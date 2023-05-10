using System.Security.Claims;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IJWTService
    {
        /// <summary>
        /// Generates a JWT for the specified user. This is used to sign the user in the event that they are trying to log in to the application
        /// </summary>
        /// <param name="user">The user to generate a JWT for</param>
        /// <returns>A JWT that can be used to sign the user in the</returns>
        string GenerateJWT(User user);
        /// <summary>
        /// Validates a JWT and returns the principal. This is a wrapper around Validate that can be used to perform validation on the JWT
        /// </summary>
        /// <param name="token">The token to validate</param>
        ClaimsPrincipal Validate(string token);
        /// <summary>
        /// Hashes the input string. This is used to determine if a file is a hash or not
        /// </summary>
        /// <param name="inputString">The string to hash.</param>
        /// <returns>The hash of the input string or null if the input string is</returns>
        string Hash(string inputString);
        /// <summary>
        /// Verifies the security credentials. This is used to verify the security credentials of a user before they are logged in.
        /// </summary>
        /// <param name="Pass">The password to verify. This can be null</param>
        /// <param name="oldPass">The old password if the user is logged in.</param>
        /// <returns>True if the verification succeeds</returns>
        bool Verify(string Pass, string oldPass);
    }
}
