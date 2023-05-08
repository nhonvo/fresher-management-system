using Application.Commons;
using Application.Syllabuses.Commands;
using Application.Syllabuses.DTO;
using Application.Syllabuses.Queries;
using Domain.Aggregate.AppResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class SyllabusController : BaseController
    {
        private readonly IMediator _mediator;
        public SyllabusController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // TODO: FIXE pagination user params or move the another layer
        [HttpGet]
        public async Task<ApiResult<Pagination<SyllabusDTO>>> Get(int pageIndex = 0, int pageSize = 10)
         => await _mediator.Send(new GetSyllabusQuery(pageIndex, pageSize));
        [HttpPost]
        public async Task<ApiResult<SyllabusDTO>> Post([FromBody] CreateSyllabusCommand request)
        => await _mediator.Send(request);
    }
}