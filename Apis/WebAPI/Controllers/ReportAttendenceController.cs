using Application.Commons;
using Application.ReportAttendances.Commands.CreateReportAttendances;
using Application.ReportAttendances.Commands.DeleteReportAttendances;
using Application.ReportAttendances.Commands.UpdateReportAttendanceStatus;
using Application.ReportAttendances.Commands.UpdateReportAttendances;
using Application.ReportAttendances.DTO;
using Application.ReportAttendances.Queries.GetreportAttendance;
using Application.ReportAttendances.Queries.GetreportAttendanceById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ReportAttendanceController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public ReportAttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Pagination<AttendanceDTO>> Get(int pageIndex = 0, int pageSize = 10)
        {
            return await _mediator.Send(new GetreportAttendanceQuery(pageIndex, pageSize));
        }
        [HttpGet("{id}")]
        public async Task<AttendanceDTO> Get(int id)
            => await _mediator.Send(new GetreportAttendanceByIdQuery(id));

        [HttpPost]
        public async Task<AttendanceDTO> Post([FromBody] CreateReportAttendancesCommand request)
            => await _mediator.Send(request);
        [HttpPut]
        public async Task<AttendanceDTO> Put([FromBody] UpdateReportAttendancesCommand request)
            => await _mediator.Send(request);
        [HttpDelete("{id}")]
        public async Task<AttendanceDTO> Delete(int id)
            => await _mediator.Send(new DeleteReportAttendancesCommand(id));

        [HttpPut("status")]
        public async Task<AttendanceDTO> ChangeStatus(ChangeAttendanceStatusCommand request)
       => await _mediator.Send(request);
    }
}
