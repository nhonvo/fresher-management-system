using Application.Commons;
using Application.Syllabuses.Commands.CreateSyllabus;
using Application.Syllabuses.DTO;
using Application.Syllabuses.Queries.GetSyllabus;
using Application.Syllabuses.Queries.GetSyllabusById;
using Application.Syllabuses.Queries.GetSyllabusName;
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
        [HttpGet("{id}")]
        public async Task<SyllabusDTO> Get(int id)
         => await _mediator.Send(new GetSyllabusByIdQuery(id));
        [HttpGet]
        public async Task<ApiResult<Pagination<SyllabusDTO>>> Get(
            string? name,
            int pageIndex = 0,
            int pageSize = 10)
         => await _mediator.Send(new GetSyllabusNameQuery(name, pageIndex, pageSize));
        [HttpPost]
        public async Task<SyllabusDTO> Post([FromBody] CreateSyllabusCommand request)
        => await _mediator.Send(request);
    }
}