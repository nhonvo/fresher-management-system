using Quartz;

namespace Application.Cronjob
{
    public class SendAttendanceMailJob : IJob
    {

        public SendAttendanceMailJob()
        {
        }

        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }

    }
}
