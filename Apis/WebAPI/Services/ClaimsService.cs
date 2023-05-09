using Application.Interfaces;
using System.Security.Claims;

namespace WebAPI.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IJWTService _jwtService;
        private string accessToken;

        public ClaimService(IHttpContextAccessor httpContextAccessor, IJWTService jwtService)
        {
            _jwtService = jwtService;
            accessToken = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;

        }
        public Guid CurrentUserId 
        {
            get
            {
                if (accessToken == null)

                    throw new Exception("No access token found!!!");

                var id = _jwtService.Validate(accessToken).Claims.FirstOrDefault(c => c.Type == "ID")?.Value;

                if (id == null)

                    throw new Exception("No user id found!!!");

                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
            }
        }
    }
}
