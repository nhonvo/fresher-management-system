using Application.AllocateSyllabus.GetSyllabusAllocate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AllocateController : BasesController
    {
        private readonly IMediator _mediator;
        public AllocateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetAllocateAsync(int id)
        //     => Ok(await _mediator.Send(new GetSyllabusTimeAllocation(id)));
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllocateWithTimeAsync(int id)
            => Ok(await _mediator.Send(new GetSyllabusTimeAllocatePercentage(id)));
    }
}
