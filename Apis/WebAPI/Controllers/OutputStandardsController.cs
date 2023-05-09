using Application.Commons;
using Application.OutputStandardes.Queries.GetOutputStandard;
using Application.OutputStandards.DTO;
using Domain.Aggregate.AppResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class OutputStandardsController : BaseController
{
    private readonly IMediator _mediator;

    public OutputStandardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ApiResult<Pagination<OutputStandardDTO>>> Get(
        int pageIndex = 0,
        int pageSize = 10)
        => await _mediator.Send(new GetOutputStandardQuery(pageIndex, pageSize));
}
