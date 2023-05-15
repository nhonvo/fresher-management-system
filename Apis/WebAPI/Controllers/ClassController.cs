using Application.Calenders.Queries.GetPagedCalendersByTrainingClassId;
using Application.Class.Commands.AddTrainer;
using Application.Class.Commands.CreateClass;
using Application.Class.Commands.DeleteClass;
using Application.Class.Commands.UpdateClass;
using Application.Class.DTO;
using Application.Class.DTOs;
using Application.Class.Queries.GetAdminClass;
using Application.Class.Queries.GetClass;
using Application.Class.Queries.GetClassProgram;
using Application.Commons;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ClassController : BasesController
    {
        private readonly IMediator _mediator;
        public ClassController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<Pagination<ClassDTO>> Get(int pageIndex = 0, int pageSize = 10)
        {
            return await _mediator.Send(new GetClassQuery(pageIndex, pageSize));
        }
        [HttpGet("{id}/Admin")]
        public async Task<Pagination<AdminClass>> GetClassAsync(int id, int pageIndex = 0, int pageSize = 10)
            => await _mediator.Send(new GetAdminClassQuery(id, pageIndex, pageSize));
        [HttpGet("{id}")]
        public async Task<ClassDTO> Get(int id)
            => await _mediator.Send(new GetClassByIdQuery(id));
        [HttpGet("{id}/Program")]
        public async Task<Pagination<ClassProgram>> GetProgram(int id, int pageIndex = 0, int pageSize = 10)
            => await _mediator.Send(new GetClassProgramQuery(id, pageIndex, pageSize));
        [HttpPost]
        [Authorize(Roles = "ClassAdmin")]
        public async Task<ClassDTO> Post([FromBody] CreateClassCommand request)
            => await _mediator.Send(request);
        [HttpPost("add-trainer")]
        [Authorize(Roles = "ClassAdmin")]
        public async Task<TrainerClassDTO> Post([FromBody] AddTrainerCommand request)
            => await _mediator.Send(request);
        [HttpPut]
        [Authorize(Roles = "ClassAdmin")]
        public async Task<ClassDTO> Put([FromBody] UpdateClassCommand request)
            => await _mediator.Send(request);
        [HttpDelete("{id}")]
        [Authorize(Roles = "ClassAdmin")]
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

        #endregion calenders
    }
}
