using Application.Commons;
using Application.UnitLessons.Commands.CreateUnitLesson;
using Application.UnitLessons.Commands.DeleteUnitLesson;
using Application.UnitLessons.Commands.UpdateUnitLesson;
using Application.UnitLessons.DTO;
using Application.UnitLessons.Queries.GetUnitLessonById;
using Application.UnitLessons.Queries.GetUnitLessons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class UnitLessonController : BasesController
    {
        private readonly IMediator _mediator;
        public UnitLessonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Pagination<UnitLessonDTO>> Get(int pageIndex = 0, int pageSize = 10)
            => await _mediator.Send(new GetUnitLessonQuery(pageIndex, pageSize));

        [HttpGet("{id}")]
        public async Task<UnitLessonDTO> Get(int id)
            => await _mediator.Send(new GetUnitLessonByIdQuery(id));

        [HttpPost]
        public async Task<UnitLessonDTO> Post([FromBody] CreateUnitLessonCommand request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<UnitLessonDTO> Put([FromBody] UpdateUnitLessonCommand request)
            => await _mediator.Send(request);

        [HttpDelete("{id}")]
        public async Task<UnitLessonDTO> Delete(int id)
            => await _mediator.Send(new DeleteUnitLessonCommand(id));
    }
}
