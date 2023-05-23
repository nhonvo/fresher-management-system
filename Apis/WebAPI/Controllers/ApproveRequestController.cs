using Application.ApproveRequests.Commands.AllowRequest;
using Application.ApproveRequests.Commands.CreateRequest;
using Application.ApproveRequests.Commands.CreateRequestCurrentUser;
using Application.ApproveRequests.DTOs;
using Application.ApproveRequests.GetApprove;
using Application.ApproveRequests.GetApproveById;
using Application.ApproveRequests.GetApproveFilter;
using Application.Commons;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    public class ApproveRequestController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public ApproveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10)
            => Ok(await _mediator.Send(new GetApproveQuery(pageIndex, pageSize)));
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _mediator.Send(new GetApproveByIdQuery(id)));
        [HttpGet("filter")]
        public async Task<IActionResult> Get(
            StatusApprove order = StatusApprove.Waiting,
            int pageIndex = 0,
            int pageSize = 10)
            => Ok(await _mediator.Send(new GetApproveFilterQuery(order, pageIndex, pageSize)));
        [HttpPost]
        public async Task<IActionResult> Post(CreateRequestCurrentUserCommand request)
            => Ok(await _mediator.Send(request));
        [HttpPost("ApproveEnroll")]
        [Authorize(Roles = "ClassAdmin, SuperAdmin")]
        public async Task<IActionResult> Post(AllowRequestCommand request)
            => Ok(await _mediator.Send(request));
    }
}
