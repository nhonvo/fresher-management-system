using Application.Commons;
using Application.Calenders.DTO;
using Application.Calenders.Queries.GetCalenderById;
using Application.Calenders.Queries.GetPagedCalenders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class CalendersController : BasesController
{
    private readonly ISender _mediator;

    public CalendersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<Pagination<CalenderDTO>> Get(int pageIndex = 0, int pageSize = 10)
        => await _mediator.Send(new GetPagedCalendersQuery(pageIndex, pageSize));

    [HttpGet("{id}")]
    public async Task<CalenderDTO> Get(int id)
        => await _mediator.Send(new GetCalenderByIdQuery(id));
}
