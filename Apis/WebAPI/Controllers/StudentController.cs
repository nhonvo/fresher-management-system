// using Application.Class.DTO;
// using Application.Class.Queries.GetClass;
// using Application.Commons;
// using Application.Student.Commands.AddStudent;
// using Application.Student.Commands.UpdateStudent;
// using Application.StudentProgresses.Queries.GetStudentProgressById;
// using Application.Students.DTO;
// using Domain.Aggregate.AppResult;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;

// namespace WebAPI.Controllers;

// public class StudentController : BaseController
// {
//     private readonly IMediator _mediator;
//     public StudentController(IMediator mediator)
//     {
//         _mediator = mediator;
//     }

//     [HttpGet("{id}/paged-student-progresses")]
//     public async Task<ApiResult<Pagination<StudentProgressDTO>>> GetPagedStudentProgressesById(int id)
//     => await _mediator.Send(new GetPagedStudentProgressesByIdQuery(id));

//     [HttpPut("add-student-fields")]
//     public async Task<StudentDTO> Add(AddStudentCommand request)
//     => await _mediator.Send(request);
//     [HttpPut("update-student-profiles")]
//     public async Task<StudentDTO> Put(UpdateStudentCommand request)
//     => await _mediator.Send(request);
// }
