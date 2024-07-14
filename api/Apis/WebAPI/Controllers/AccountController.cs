using Application.Account.Commands.AddRole;
using Application.Account.Commands.ChangPassword;
using Application.Account.Commands.CreateAccount;
using Application.Account.Commands.CreateAccountTrainer;
using Application.Account.Commands.Login;
using Application.Account.Commands.Register;
using Application.Account.DTOs;
using Application.Account.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("GetUserByToken")]
        public async Task<AccountDTO> GetUserByToken()
        => await _mediator.Send(new GetTokenByIdQuery());
        [HttpPost("Login")]
        public async Task<AccountDTO> LoginAsync([FromBody] LoginCommand request)
            => await _mediator.Send(request);

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand request)
        => Ok(await _mediator.Send(request));

        [Authorize]
        [HttpPut("change-password")]
        public async Task<AccountDTO> ChangePassword(ChangePasswordCommand request)
            => await _mediator.Send(request);
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("add-role")]
        public async Task<AccountDTO> AddRole([FromBody] AddRoleCommand request)
            => await _mediator.Send(request);
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("create-account-admin")]
        public async Task<AccountDTO> CreateAccount([FromBody] CreateClassAdminCommand request)
            => await _mediator.Send(request);
        [Authorize(Roles = "ClassAdmin, SuperAdmin")]
        [HttpPost("create-account-trainer")]
        public async Task<AccountDTO> CreateAccount([FromBody] CreateAccountTrainerCommand request)
            => await _mediator.Send(request);
    }
}