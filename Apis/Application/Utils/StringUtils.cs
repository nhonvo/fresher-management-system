using Application.Interfaces;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;

namespace Application.Utils
{
    public static class StringUtils
    {
        public static string Hash(this string inputString)
            => BCrypt.Net.BCrypt.HashPassword(inputString);
    }
}