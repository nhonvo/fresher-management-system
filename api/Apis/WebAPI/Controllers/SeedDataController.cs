using Application.SeedData.Queries.SeedData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class SeedDataController : BasesController
    {
        private readonly IMediator _mediator;
        public SeedDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task Get()
            => await _mediator.Send(new SeedDataQuery());
    }
}

