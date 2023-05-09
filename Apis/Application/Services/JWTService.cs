using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain;
using Domain.Entities;
using Domain.Entities.Users;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class JWTService : IJWTService
    {
        private readonly AppConfiguration _configuration;
        public JWTService(AppConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJWT(User user)
        {
            var issuer = _configuration.Jwt.Issuer;
            var audience = _configuration.Jwt.Audience;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Jwt.Key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("ID", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("Phone", user.Phone),
                new Claim(JwtRegisteredClaimNames.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    audience: audience,
                    issuer: issuer,
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public ClaimsPrincipal Validate(string token)
        {
            IdentityModelEventSource.ShowPII = true;
            TokenValidationParameters validationParameters = new()
            {
                ValidateLifetime = true,
                ValidAudience = _configuration.Jwt.Audience,
                ValidIssuer = _configuration.Jwt.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Jwt.Key))
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);

            return principal;
        }
        public string Hash(string inputString) => BCrypt.Net.BCrypt.HashPassword(inputString);
        public bool Verify(string Pass, string oldPass) => BCrypt.Net.BCrypt.Verify(Pass, oldPass);
    }
}
