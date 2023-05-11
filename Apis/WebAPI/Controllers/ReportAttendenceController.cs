using Application.Commons;
using Application.ReportAttendances.Commands.CreateReportAttendances;
using Application.ReportAttendences.Commands.DeleteReportAttendences;
using Application.ReportAttendences.Commands.UpdateReportAttendanceStatus;
using Application.ReportAttendences.Commands.UpdateReportAttendences;
using Application.ReportAttendences.DTO;
using Application.ReportAttendences.Queries.GetReportAttendence;
using Application.ReportAttendences.Queries.GetReportAttendenceById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ReportAttendenceController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public ReportAttendenceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Pagination<ReportAttendanceDTO>> Get(int pageIndex = 0, int pageSize = 10)
        {
            return await _mediator.Send(new GetReportAttendenceQuery(pageIndex, pageSize));
        }
        [HttpGet("{id}")]
        public async Task<ReportAttendanceDTO> Get(int id)
            => await _mediator.Send(new GetReportAttendenceByIdQuery(id));

        [HttpPost]
        public async Task<ReportAttendanceDTO> Post([FromBody] CreateReportAttendancesCommand request)
            => await _mediator.Send(request);
        [HttpPut]
        public async Task<ReportAttendanceDTO> Put([FromBody] UpdateReportAttendancesCommand request)
            => await _mediator.Send(request);
        [HttpDelete("{id}")]
        public async Task<ReportAttendanceDTO> Delete(int id)
            => await _mediator.Send(new DeleteReportAttendencesCommand(id));

        [HttpPut("status")]
        public async Task<ReportAttendanceDTO> ChangeStatus(ChangeAttendanceStatusCommand request)
       => await _mediator.Send(request);
    }
}
