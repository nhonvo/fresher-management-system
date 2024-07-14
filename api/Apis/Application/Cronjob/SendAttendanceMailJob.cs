using Application.Emails.Commands.SendMailAttendance;
using MediatR;
using Quartz;

namespace Application.Cronjob
{
    public class SendAttendanceMailJob : IJob
    {
        private readonly IMediator _mediator;

        public SendAttendanceMailJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _mediator.Send(new SendMailAttendanceCommand());
        }

    }
}
