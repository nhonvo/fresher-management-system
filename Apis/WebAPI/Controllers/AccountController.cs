using Application.Account.Commands.Login;
using Application.Account.Commands.Register;
using Application.Account.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AccountController : CustomBaseController
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
    // TODO: ADD session service , mail service
}