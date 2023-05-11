using Application.Commons;
using Application.Units.Commands.CreateUnit;
using Application.Units.Commands.DeleteUnit;
using Application.Units.Commands.UpdateUnit;
using Application.Units.DTO;
using Application.Units.Queries.GetUnitById;
using Application.Units.Queries.GetUnitByName;
using Application.Units.Queries.GetUnits;
using Domain.Aggregate.AppResult;
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
        public async Task<ApiResult<Pagination<UnitDTO>>> Get(int pageIndex = 0, int pageSize = 10)
            => await _mediator.Send(new GetUnitQuery(pageIndex, pageSize));

        [HttpGet("{id}")]
        public async Task<UnitDTO> Get(int id)
            => await _mediator.Send(new GetUnitByIdQuery(id));

        [HttpGet("{name}")]
        public async Task<UnitDTO> Get(string name)
            => await _mediator.Send(new GetUnitByNameQuery(name));

        [HttpPost]
        public async Task<UnitDTO> Post([FromBody] CreateUnitCommand request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<UnitDTO> Put([FromBody] UpdateUnitCommand request)
            => await _mediator.Send(request);

        [HttpDelete("{id}")]
        public async Task<UnitDTO> Delete(int id)
            => await _mediator.Send(new DeleteUnitCommand(id));
    }
}
