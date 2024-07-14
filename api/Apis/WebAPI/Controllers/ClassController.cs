using Application.Calenders.Queries.GetPagedCalendersByTrainingClassId;
using Application.Class.Commands.AddTrainer;
using Application.Class.Commands.AddTrainProgramToClass;
using Application.Class.Commands.CreateClass;
using Application.Class.Commands.DeleteClass;
using Application.Class.Commands.SetCalenders;
using Application.Class.Commands.UpdateClass;
using Application.Class.DTOs;
using Application.Class.Queries.GetAdminClass;
using Application.Class.Queries.GetClass;
using Application.Class.Queries.GetClassById;
using Application.Class.Queries.GetClassDetail;
using Application.Class.Queries.GetClassDuration;
using Application.Class.Queries.GetClassProgram;
using Application.Commons;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    public class ClassController : BasesController
    {
        private readonly IMediator _mediator;
        public ClassController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10)
        => Ok(await _mediator.Send(new GetClassQuery() { PageIndex = pageIndex, PageSize = pageSize }));

        [HttpGet("{id}/Admin")]
        public async Task<Pagination<Application.Class.DTOs.ClassRelated>> GetClassAsync(int id, int pageIndex = 0, int pageSize = 10)
            => await _mediator.Send(new GetAdminClassQuery(id, pageIndex, pageSize));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        => Ok(await _mediator.Send(new GetClassDetailQuery(id)));

        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> GetDetail(int id)
            => Ok(await _mediator.Send(new GetClassByIdQuery(id)));
        [HttpGet("{id}/Program")]
        public async Task<Pagination<ClassProgram>> GetProgram(int id, int pageIndex = 0, int pageSize = 10)
            => await _mediator.Send(new GetClassProgramQuery(id, pageIndex, pageSize));
        [HttpPost]
        [Authorize(Roles = "ClassAdmin, SuperAdmin")]
        public async Task<ClassDTO> Post([FromBody] CreateClassCommand request)
            => await _mediator.Send(request);
        [HttpPost("TrainingProgram")]
        [Authorize(Roles = "ClassAdmin, SuperAdmin")]
        public async Task<ClassDTO> Post([FromBody] AddTrainProgramToClassCommand request)
            => await _mediator.Send(request);
        [HttpPost("add-trainer")]
        [Authorize(Roles = "ClassAdmin, SuperAdmin")]
        public async Task<TrainerClassDTO> Post([FromBody] AddTrainerCommand request)
            => await _mediator.Send(request);
        [HttpPut]
        [Authorize(Roles = "ClassAdmin, SuperAdmin")]
        public async Task<ClassDTO> Put([FromBody] UpdateClassCommand request)
            => await _mediator.Send(request);
        [HttpDelete("{id}")]
        [Authorize(Roles = "ClassAdmin, SuperAdmin")]
        public async Task<ClassDTO> Delete(int id)
            => await _mediator.Send(new DeleteClassCommand(id));

        #region calenders

        [HttpGet("{id}/calendars")]
        public async Task<IActionResult> GetPagedCalendersByTrainingClassId(
            int id,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10)
            => Ok(await _mediator.Send(new GetPagedCalendersByTrainingClassIdQuery()
            {
                TrainingClassId = id,
                PageIndex = pageIndex,
                PageSize = pageSize
            }));

        [HttpPost("{id}/calenders/set-all")]
        public async Task<IActionResult> SetCalendersOfTrainingClass(
            int id,
            [FromBody] SetCalendersOfTrainingClassCommand command)
        {
            command.TrainingClassId = id;
            return Ok(await _mediator.Send(command));
        }

        #endregion calenders

        #region duration

        [HttpGet("{id}/duration")]
        public async Task<IActionResult> GetClassDuration(int id)
            => Ok(await _mediator.Send(new GetClassDurationQuery(id)));

        #endregion duration
    }
}
