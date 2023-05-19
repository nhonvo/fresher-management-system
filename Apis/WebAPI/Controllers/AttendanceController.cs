using System.Net;
using Application.Commons;
using Application.Attendances.Commands.CreateAttendances;
using Application.Attendances.Commands.DeleteAttendances;
using Application.Attendances.Commands.UpdateAttendanceStatus;
using Application.Attendances.Commands.UpdateAttendances;
using Application.Attendances.DTO;
using Application.Attendances.Queries.GetAttendance;
using Application.Attendances.Queries.GetAttendanceById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    public class AttendanceController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        ///done  public Task<bool> CreateAbsentRequestAsync(string reason);
        ///done public Task<bool> ApproveAbsentRequestAsync(int id, string status);
        ///done public Task<TMS> GetAbsentRequestByIdAsync(int id);
        ///done public Task<List<TMSListViewModel>> GetAllAbsentRequestsAsync();
        /// public Task<List<TMSPendingListViewModel>> GetAllPendingAbsentRequestsAsync();
        /// public Task<List<TMSListViewModel>> SearchAbsentRequest(string searchBy, string searchElement);
        /// public Task SendAttendanceMailAsync();
        /// </summary>
        /// Task<bool> EditAttendanceAsync(EditAttendanceViewModel editAttendanceViewModel,int id);
        /// Task<List<AttendanceViewModel>> GetAllAttendanceAsync(int id);
        /// Task<AttendanceViewModel> TakeAttendance(TakeAttendanceModel view);
        /// Task<ValidationResult> ValidateTakeAttendanceAsync(TakeAttendanceModel attendance);
        [HttpGet]
        public async Task<Pagination<AttendanceDTO>> Get(int pageIndex = 0, int pageSize = 10)
        {
            return await _mediator.Send(new GetAttendanceQuery(pageIndex, pageSize));
        }
        [HttpGet("{id}")]
        public async Task<AttendanceDTO> Get(int id)
            => await _mediator.Send(new GetAttendanceByIdQuery(id));

        [HttpPost]
        public async Task<AttendanceDTO> Post([FromBody] CreateAttendancesCommand request)
            => await _mediator.Send(request);
        [HttpPut]
        public async Task<AttendanceDTO> Put([FromBody] UpdateAttendancesCommand request)
            => await _mediator.Send(request);
        // [HttpDelete("{id}")]
        // public async Task<AttendanceDTO> Delete(int id)
        //     => await _mediator.Send(new DeleteAttendancesCommand(id));

        [HttpPut("ApproveAbsent")]
        [Authorize(Roles = "ClassAdmin")]
        public async Task<AttendanceDTO> ApproveAbsent(ApproveAbsentCommand request)
            => await _mediator.Send(request);
    }
}
