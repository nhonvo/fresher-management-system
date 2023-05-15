using Application.Interfaces;
using System.Security.Claims;

namespace WebAPI.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext != null)
            {
                var id = httpContextAccessor.HttpContext.User.FindFirstValue("ID");
                CurrentUserId = id == null ? -1 : int.Parse(id);
            }
        }
        public int CurrentUserId { get; }
    }
}
