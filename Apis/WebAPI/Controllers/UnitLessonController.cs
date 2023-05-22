using Application.Commons;
using Application.Lessons.Commands.CreateUnitLesson;
using Application.Lessons.Commands.DeleteUnitLesson;
using Application.Lessons.Commands.UpdateUnitLesson;
using Application.Lessons.DTO;
using Application.Lessons.Queries.GetUnitLessonById;
using Application.Lessons.Queries.GetUnitLessons;
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
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10)
            => Ok(await _mediator.Send(new GetUnitLessonQuery(pageIndex, pageSize)));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _mediator.Send(new GetUnitLessonByIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUnitLessonCommand request)
            => Ok(await _mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUnitLessonCommand request)
            => Ok(await _mediator.Send(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _mediator.Send(new DeleteUnitLessonCommand(id)));
    }
}
