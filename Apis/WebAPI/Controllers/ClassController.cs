using Application.Commons;
using Application.Syllabuses.DTO;
using Application.Syllabuses.Queries.GetSyllabus;
using Domain.Aggregate.AppResult;
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
        public async Task<ApiResult<Pagination<SyllabusDTO>>> Get(int pageIndex = 0, int pageSize = 10)
        => await _mediator.Send(new GetSyllabusQuery(pageIndex, pageSize));
    }
}
