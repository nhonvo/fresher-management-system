using Application.Attendances.Commands.ApproveAbsent;
using Application.Attendances.Commands.CreateAttendances;
using Application.Attendances.Commands.UpdateAttendances;
using Application.Attendances.DTO;
using Application.Attendances.Queries.GetAttendanceById;
using Application.Attendances.Queries.GetAttendanceFilterRequest;
using Application.Attendances.Queries.GetAttendanceOfClass;
using Application.Attendances.Queries.GetAttendanceRequest;
using Application.Attendances.Queries.SearchAttendanceRequest;
using Application.Commons;
using Application.Emails.Commands.SendMailAttendance;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    public class AttendanceController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 0, int pageSize = 10)
        {
            return Ok(await _mediator.Send(new GetAttendanceRequestQuery(pageIndex, pageSize)));
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search(
            string? searchString,
            SortType sortType = SortType.Ascending,
            int pageIndex = 0,
            int pageSize = 10)
        {
            return Ok(await _mediator.Send(new SearchAttendanceRequestQuery(
                searchString,
                sortType,
                pageIndex,
                pageSize)));
        }
        [HttpGet("Filter")]
        public async Task<IActionResult> GetPending(
            StatusAttendanceApprove? status,
            int pageIndex = 0,
            int pageSize = 10)
        {
            return Ok(await _mediator.Send(new GetAttendanceFilterRequestQuery(status, pageIndex, pageSize)));
        }
        // [HttpGet("attendance-class")]
        // public async Task<IActionResult> GetAttendanceClass(int pageIndex = 0, int pageSize = 10)
        // {
        //     return Ok(await _mediator.Send(new GetAttendanceOfClassQuery(pageIndex, pageSize)));
        // }
        // [HttpGet("{id}")]
        // public async Task<IActionResult> Get(int id)
        // {
        //     return Ok(await _mediator.Send(new GetAttendanceByIdQuery(id)));
        // }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateAttendancesCommand request)
        {

            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateAttendancesCommand request)
        {

            return Ok(await _mediator.Send(request));
        }
        [HttpPut("ApproveAbsent")]
        [Authorize(Roles = "ClassAdmin, SuperAdmin")]
        public async Task<ActionResult> ApproveAbsent(ApproveAbsentCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
