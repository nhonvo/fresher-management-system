// using Application.Commons;
// using Application.TestAssessments.Commands.CreateTestAssessment;
// using Application.TestAssessments.Commands.DeleteTestAssessment;
// using Application.TestAssessments.Commands.UpdateTestAssessment;
// using Application.TestAssessments.DTO;
// using Application.TestAssessments.Queries.GetPagedTestAssessments;
// using Application.TestAssessments.Queries.GetTestAssessmentById;
// using Domain.Aggregate.AppResult;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;

// namespace WebAPI.Controllers;

// [Route("api/[controller]")]
// [ApiController]
// public class TestAssessmentController : ControllerBase
// {
//     private readonly IMediator _mediator;
//     public TestAssessmentController(IMediator mediator)
//     {
//         _mediator = mediator;
//     }

//     [HttpGet]
//     public async Task<ApiResult<Pagination<TestAssessmentDTO>>> Get(int pageIndex = 0, int pageSize = 10)
//      => await _mediator.Send(new GetPagedTestAssessmentsQuery(pageIndex, pageSize));

//     [HttpGet("{id}")]
//     public async Task<TestAssessmentDTO> Get(int id)
//      => await _mediator.Send(new GetTestAssessmentByIdQuery(id));
//     [HttpPost]
//     public async Task<TestAssessmentDTO> Post(CreateTestAssessmentCommand request)
//      => await _mediator.Send(request);
//     [HttpPut]
//     public async Task<TestAssessmentDTO> Put(UpdateTestAssessmentCommand request)
//      => await _mediator.Send(request);
//     [HttpDelete("{id}")]
//     public async Task<TestAssessmentDTO> Delete(int id)
//      => await _mediator.Send(new DeleteTestAssessmentCommand(id));
// }
