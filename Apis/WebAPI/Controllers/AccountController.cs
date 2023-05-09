using Application.Account.Commands.Login;
using Application.Account.Commands.Register;
using Application.Account.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Login")]
        public async Task<AccountDTO> LoginAsync([FromBody] LoginCommand request)
            => await _mediator.Send(request);

        [HttpPost("Register")]
        public async Task<AccountDTO> RegisterAsync([FromBody] RegisterCommand request)
            => await _mediator.Send(request);
    }

    // TODO: Seed data
    // TODO: allow create role for user 
    // TODO: Send mail when user register
    // TODO: Method: forget password,
    /// change password,
    /// filter user,
    /// validate token(fe),
    /// save token in session, 
    /// Logout 
}