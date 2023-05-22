using Application.Commons;
using Application.Units.Commands.CreateUnit;
using Application.Units.Commands.DeleteUnit;
using Application.Units.Commands.UpdateUnit;
using Application.Units.DTOs;
using Application.Units.Queries.GetUnitById;
using Application.Units.Queries.GetUnitByName;
using Application.Units.Queries.GetUnits;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UnitController : BasesController
    {
        private readonly IMediator _mediator;
        public UnitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10)
            => Ok(await _mediator.Send(new GetUnitQuery(pageIndex, pageSize)));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _mediator.Send(new GetUnitByIdQuery(id)));

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
            => Ok(await _mediator.Send(new GetUnitByNameQuery(name)));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUnitCommand request)
            => Ok(await _mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUnitCommand request)
            => Ok(await _mediator.Send(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _mediator.Send(new DeleteUnitCommand(id)));
    }
}
