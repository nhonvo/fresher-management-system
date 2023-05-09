using Application.Class.DTO;
using Application.Class.Queries.GetClass;
using Application.Commons;
using Application.StudentProgresses.Queries.GetStudentProgressById;
using Application.Students.DTO;
using Domain.Aggregate.AppResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class StudentController : BaseController
{
    private readonly IMediator _mediator;
    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}/paged-student-progresses")]
    public async Task<ApiResult<Pagination<StudentProgressDTO>>> GetPagedStudentProgressesById(int id)
    => await _mediator.Send(new GetPagedStudentProgressesByIdQuery(id));
}
