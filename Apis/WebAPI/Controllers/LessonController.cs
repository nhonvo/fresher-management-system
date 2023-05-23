using Application.Lessons.Commands.AddTrainingMaterialsToLesson;
using Application.Lessons.Commands.CreateUnitLesson;
using Application.Lessons.Commands.DeleteUnitLesson;
using Application.Lessons.Commands.UpdateUnitLesson;
using Application.Lessons.Queries.GetPagedTrainingMaterialsByLessonId;
using Application.Lessons.Queries.GetUnitLessonById;
using Application.Lessons.Queries.GetUnitLessons;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    public class LessonController : BasesController
    {
        private readonly IMediator _mediator;
        public LessonController(IMediator mediator)
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

        [HttpGet("{id}/training-materials")]
        public async Task<IActionResult> GetAsync(
            int id,
            string? keyword,
            SortType sortType = SortType.Ascending,
            int pageIndex = 0,
            int pageSize = 10)
        => Ok(await _mediator.Send(new GetPagedTrainingMaterialsByLessonIdQuery()
        {
            LessonId = id,
            Keyword = keyword,
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortType = sortType
        }));

        [HttpPost("{id}/training-materials")]
        public async Task<IActionResult> AddTrainingMaterialsToTestAssessment(
            int id,
            [FromForm] AddTrainingMaterialsToLessonCommand request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
    }
}
