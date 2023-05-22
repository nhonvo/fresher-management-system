using Application.Commons;
using Application.TrainingPrograms.Commands.AddOneSyllabusToTrainingProgram;
using Application.TrainingPrograms.Commands.CreateTrainingProgram;
using Application.TrainingPrograms.Commands.DeleteTrainingProgram;
using Application.TrainingPrograms.Commands.DuplicateTrainProgram;
using Application.TrainingPrograms.Commands.RemoveOneSyllabusToTrainingProgram;
using Application.TrainingPrograms.Commands.UpdateTrainingProgram;
using Application.TrainingPrograms.DTOs;
using Application.TrainingPrograms.Queries.GetPagedSyllabusesByTraningProgramId;
using Application.TrainingPrograms.Queries.GetTrainingProgram;
using Application.TrainingPrograms.Queries.GetTrainingProgramById;
using Application.TrainingPrograms.Queries.GetTrainingProgramByIdRelated;
using Application.TrainingPrograms.Queries.GetTrainingProgramRelated;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class TrainingProgramController : BasesController
    {
        private readonly IMediator _mediator;
        public TrainingProgramController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // [HttpGet]
        // public async Task<IActionResult> GetAsync(int pageIndex = 0, int pageSize = 10)
        //     => Ok(await _mediator.Send(new GetTrainingProgramQuery(pageIndex, pageSize)));
        [HttpGet]
        public async Task<IActionResult> GetAsync(string? keyword,
            SortType sortType = SortType.Ascending,
            int pageIndex = 0,
            int pageSize = 10)
            => Ok(await _mediator.Send(new GetTrainingProgramRelatedQuery(keyword, sortType, pageIndex, pageSize)));
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
            => Ok(await _mediator.Send(new GetTrainingProgramByIdQuery(id)));
        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> GetDetailAsync(int id)
            => Ok(await _mediator.Send(new GetTrainingProgramByIdRelatedQuery(id)));
        [HttpPost("Duplicate/{id}")]
        // [Authorize(Roles = "ClassAdmin, Trainer")]
        public async Task<IActionResult> Duplicate(int id)
            => Ok(await _mediator.Send(new DuplicateTrainProgramCommand(id)));
        [HttpPost]
        [Authorize(Roles = "ClassAdmin")]
        public async Task<IActionResult> Post(CreateTrainingProgramCommand request)
            => Ok(await _mediator.Send(request));
        [HttpGet("{trainingProgramId}/Syllabuses")]
        public async Task<IActionResult> GetPagedSyllabusesByTraningProgramId(
            int trainingProgramId,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10)
        => Ok(await _mediator.Send(new GetPagedSyllabusesByTraningProgramIdQuery()
        {
            TrainingProgramId = trainingProgramId,
            PageIndex = pageIndex,
            PageSize = pageSize
        }));
        [HttpPost("{trainingProgramId}/Syllabus")]
        [Authorize(Roles = "ClassAdmin")]
        public async Task<IActionResult> AddOneSyllabusToTrainingProgram(int syllabusId, int trainingProgramId)
        => Ok(await _mediator.Send(new AddOneSyllabusToTrainingProgramCommand(syllabusId, trainingProgramId)));
        [HttpDelete("{trainingProgramId}/Syllabus")]
        [Authorize(Roles = "ClassAdmin")]
        public async Task<IActionResult> RemnoveOneSyllabusToTrainingProgram(int syllabusId, int trainingProgramId)
        => Ok(await _mediator.Send(new RemoveOneSyllabusToTrainingProgramCommand(syllabusId, trainingProgramId)));
        [HttpPut]
        [Authorize(Roles = "ClassAdmin")]
        public async Task<IActionResult> Put(UpdateTrainingProgramCommand request)
            => Ok(await _mediator.Send(request));
        [HttpDelete("{id}")]
        [Authorize(Roles = "ClassAdmin")]
        public async Task<IActionResult> Delete(DeleteTrainingProgramCommand request)
            => Ok(await _mediator.Send(request));
    }
}
