using Application.Commons;
using Application.TestAssessmentes.Queries.GetPagedTestAssessments;
using Application.TestAssessments.DTO;
using Application.TestAssessments.Queries.GetTestAssessmentById;
using Domain.Aggregate.AppResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestAssessmentController : ControllerBase
{
    private readonly IMediator _mediator;
    public TestAssessmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ApiResult<Pagination<TestAssessmentDTO>>> Get(int pageIndex = 0, int pageSize = 10)
     => await _mediator.Send(new GetPagedTestAssessmentsQuery(pageIndex, pageSize));

    [HttpGet("{id}")]
    public async Task<TestAssessmentDTO> Get(int id)
     => await _mediator.Send(new GetTestAssessmentByIdQuery(id));
}
