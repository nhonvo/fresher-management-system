using Application.ApproveRequests.Commands.AllowRequest;
using Application.ApproveRequests.Commands.CreateRequest;
using Application.ApproveRequests.DTOs;
using Application.ApproveRequests.GetApprove;
using Application.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ApproveRequestController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public ApproveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<Pagination<ApproveRequestDTO>> Get(int pageIndex = 0, int pageSize = 10)
            => await _mediator.Send(new GetApproveQuery(pageIndex, pageSize));
        [HttpPost]
        public async Task<ApproveRequestDTO> Post(CreateRequestCommand request)
            => await _mediator.Send(request);
        [HttpPost("ApproveEnroll")]
        public async Task<ApproveRequestDTO> Post(AllowRequestCommand request)
            => await _mediator.Send(request);
    }
}
