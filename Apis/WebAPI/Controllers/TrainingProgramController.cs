using Application.Commons;
using Application.TrainingPrograms.Commands.AddOneSyllabusToTrainingProgram;
using Application.TrainingPrograms.Commands.CreateTrainingProgram;
using Application.TrainingPrograms.Commands.DeleteTrainingProgram;
using Application.TrainingPrograms.Commands.RemoveOneSyllabusToTrainingProgram;
using Application.TrainingPrograms.Commands.UpdateTrainingProgram;
using Application.TrainingPrograms.DTOs;
using Application.TrainingPrograms.Queries.GetTrainingProgram;
using Application.TrainingPrograms.Queries.GetTrainingProgramById;
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
        [HttpGet]
        public async Task<Pagination<TrainingProgramDTO>> GetAsync(int pageIndex = 0, int pageSize = 10)
            => await _mediator.Send(new GetTrainingProgramQuery(pageIndex, pageSize));
        [HttpGet("{id}")]
        public async Task<TrainingProgramDTO> GetAsync(int id)
            => await _mediator.Send(new GetTrainingProgramByIdQuery(id));
        [HttpPost]
        [Authorize(Roles = "ClassAdmin")]
        public async Task<TrainingProgramDTO> Post(CreateTrainingProgramCommand request)
            => await _mediator.Send(request);
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
        public async Task<TrainingProgramDTO> Put(UpdateTrainingProgramCommand request)
            => await _mediator.Send(request);
        [HttpDelete("{id}")]
        [Authorize(Roles = "ClassAdmin")]
        public async Task<TrainingProgramDTO> Delete(DeleteTrainingProgramCommand request)
            => await _mediator.Send(request);
    }
}
