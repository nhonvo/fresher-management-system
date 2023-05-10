using Application.Commons;
using Application.ReportAttendences.Commands.CreateReportAttendences;
using Application.ReportAttendences.Commands.DeleteReportAttendences;
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
        public async Task<Pagination<ReportAttendenceDTO>> Get(int pageIndex = 0, int pageSize = 10)
        {
            return await _mediator.Send(new GetReportAttendenceQuery(pageIndex, pageSize));
        }
        [HttpGet("{id}")]
        public async Task<ReportAttendenceDTO> Get(int id)
            => await _mediator.Send(new GetReportAttendenceByIdQuery(id));

        [HttpPost]
        public async Task<ReportAttendenceDTO> Post([FromBody] CreateReportAttendencesCommand request)
            => await _mediator.Send(request);
        [HttpPut]
        public async Task<ReportAttendenceDTO> Put([FromBody] UpdateReportAttendencesCommand request)
            => await _mediator.Send(request);
        [HttpDelete("{id}")]
        public async Task<ReportAttendenceDTO> Delete(int id)
            => await _mediator.Send(new DeleteReportAttendencesCommand(id));

        [HttpPut("status")]
        public async Task<ReportAttendenceDTO> ChangeStatus(ChangeAttendanceStatusCommand request)
       => await _mediator.Send(request);
    }
}
