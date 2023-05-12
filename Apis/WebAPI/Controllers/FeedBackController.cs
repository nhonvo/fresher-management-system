using Application.Commons;
using Application.FeedBacks.Commands.CreateFeedBack;
using Application.FeedBacks.Commands.DeleteFeedBack;
using Application.FeedBacks.Commands.UpdateFeedBack;
using Application.FeedBacks.DTO;
using Application.FeedBacks.Queries.GetFeedBackById;
using Application.FeedBacks.Queries.GetFeedbackByTrainee;
using Application.FeedBacks.Queries.GetFeedBacks;
using Domain.Aggregate.AppResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class FeedBackController : BasesController
    {
        private readonly IMediator _mediator;
        public FeedBackController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResult<Pagination<FeedBackDTO>>> Get(int pageIndex = 0, int pageSize = 10)
            => await _mediator.Send(new GetFeedBackQuery(pageIndex, pageSize));

        [HttpGet("{id}")]
        public async Task<FeedBackDTO> Get(int id)
            => await _mediator.Send(new GetFeedBackByIdQuery(id));

        [HttpGet("{name}")]
        public async Task<Pagination<FeedBackDTO>> Get(string name)
            => await _mediator.Send(new GetFeedBackByTrainerQuery(name));

        [HttpPost]
        public async Task<FeedBackDTO> Post([FromBody] CreateFeedBackCommand request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<FeedBackDTO> Put([FromBody]UpdateFeedBackCommand request)
            => await _mediator.Send(request);

        [HttpDelete("{id}")]
        public async Task<FeedBackDTO> Delete(int id)
            => await _mediator.Send(new DeleteFeedBackCommand(id));

    }
}
