using Application.Class.Commands.CreateClass;
using Application.Class.Commands.DeleteClass;
using Application.Class.Commands.UpdateClass;
using Application.Class.DTO;
using Application.Class.Queries.GetClass;
using Application.Class.Queries.GetClassProgram;
using Application.Commons;
using Application.Syllabuses.Commands.CreateSyllabus;
using Application.Syllabuses.DTO;
using Application.Syllabuses.Queries.GetSyllabus;
using Domain.Aggregate.AppResult;
using Infrastructures.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ClassController : BaseController
    {
        private readonly IMediator _mediator;
        public ClassController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ApiResult<Pagination<ClassDTO>>> Get(int pageIndex = 0, int pageSize = 10)
        => await _mediator.Send(new GetClassQuery(pageIndex, pageSize));
        [HttpGet("Program")]
        public async Task<ApiResult<Pagination<ClassProgram>>> GetProgram(int pageIndex = 0, int pageSize = 10)
        => await _mediator.Send(new GetClassProgramQuery(pageIndex, pageSize));
        [HttpPost]
        public async Task<ClassDTO> Post([FromBody] CreateClassCommand request)
         => await _mediator.Send(request);
        [HttpPut]
        public async Task<ClassDTO> Put([FromBody] UpdateClassCommand request)
         => await _mediator.Send(request);
        [HttpDelete]
        public async Task<ClassDTO> Delete([FromBody] DeleteClassCommand request)
         => await _mediator.Send(request);
    }
}
