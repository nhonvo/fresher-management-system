using Application.Commons;
using Application.Student.Commands.AddStudent;
using Application.Student.Commands.UpdateStudent;
using Application.StudentProgresses.Queries.GetPagedStudentProgressesById;
using Application.Students.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Authorize]
public class StudentController : BasesController
{
    private readonly IMediator _mediator;
    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}/paged-student-progresses")]
    public async Task<Pagination<StudentProgressDTO>> GetPagedStudentProgressesById(
        int id,
        [FromQuery] int pageIndex = 0,
        [FromQuery] int pageSize = 10)
        => await _mediator.Send(new GetPagedStudentProgressesByIdQuery()
        {
            Id = id,
            PageIndex = pageIndex,
            PageSize = pageSize
        });

    [HttpPut("add-student-fields")]
    public async Task<StudentDTO> Add(AddStudentCommand request)
    => await _mediator.Send(request);

    [HttpPut("update-student-profiles")]
    public async Task<StudentDTO> Put(UpdateStudentCommand request)
    => await _mediator.Send(request);
}
