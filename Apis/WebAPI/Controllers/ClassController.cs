using Application.Class.Commands.CreateClass;
using Application.Class.DTO;
using Application.Class.Queries.GetClass;
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
        [HttpPost]
        public async Task<ClassDTO> Post([FromBody] CreateClassCommand request)
         => await _mediator.Send(request);
    }
}
