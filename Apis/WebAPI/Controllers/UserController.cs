using Application.Commons;
using Application.Users.DTO;
using Application.Users.GetProfile.Queries;
using Application.Users.GetUser.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<Pagination<UserDTO>> GetAsync(int pageIndex = 0, int pageSize = 10)
            => await _mediator.Send(new GetUserQuery(pageIndex, pageSize));

        [HttpGet("Profile")]
        [Authorize]
        public async Task<UserDTO> GetAsync()
            => await _mediator.Send(new GetProfileQuery());

    }
}
