using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Utils
{
    public static class TokenUtils
    {
        public static string GenerateToken(this User user, DateTime now, string issuer, string audience, string key)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("ID", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("Email", user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: now.AddDays(1),
                    audience: audience,
                    issuer: issuer,
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static bool Verify(string Pass, string oldPass)
            => BCrypt.Net.BCrypt.Verify(Pass, oldPass);

    }
}