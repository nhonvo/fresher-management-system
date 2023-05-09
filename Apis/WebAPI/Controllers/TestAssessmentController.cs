using MediatR;
using Microsoft.AspNetCore.Http;
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

}

