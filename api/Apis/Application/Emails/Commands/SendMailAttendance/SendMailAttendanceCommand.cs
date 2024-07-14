using Application.Attendances.DTO;
using Application.Attendances.Queries.GetAttendanceEachClass;
using Application.Attendances.Queries.GetAttendanceOfClass;
using Application.Emails.Commands.SendMail;
using Application.Emails.Queries;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Emails.Commands.SendMailAttendance
{
    public record SendMailAttendanceCommand : IRequest;
    public class SendMailAttendanceHandler : IRequestHandler<SendMailAttendanceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        private readonly IMediator _mediator;

        public SendMailAttendanceHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
            _mediator = mediator;
        }

        public async Task Handle(SendMailAttendanceCommand request, CancellationToken cancellationToken)
        {
            var trainingClass = await _mediator.Send(new GetAttendanceOfClassQuery(0, int.MaxValue));
            List<string> email = trainingClass.Items
                .SelectMany(item => item.ClassStudent.TrainingClass.ClassAdmins.Select(admin => admin.Admin.Email))
                .ToList();

            List<AttendanceRelatedTrainingClassDTO> adminTrainingClasses = trainingClass.Items
                .Where(item => item.ClassStudent.TrainingClass.ClassAdmins.Any(admin => email.Contains(admin.Admin.Email)))
                .Select(item => item.ClassStudent.TrainingClass)
                .ToList();

            var to = new List<string>();
            var title = $"Attendance Report for {DateTime.Today:d}";
            var sign = "Admin";
            var subject = $"Attendance Report for {DateTime.Today:d}";

            foreach (var entry in adminTrainingClasses)
            {
                foreach (var admin in entry.ClassAdmins)
                {
                    var speech = $"Dear {admin.Admin.Name},<br><br>The following trainees were absent in your class today ({DateTime.Today:d}):<br>";

                    to.Clear();
                    to.Add(admin.Admin.Email);

                    var attendanceQuery = new GetAttendanceEachClassQuery(entry.Id, 0, int.MaxValue);
                    var attendanceResponse = await _mediator.Send(attendanceQuery);

                    var mainContent = $"Hello {admin.Admin.Name},<br><br>";
                    mainContent += $"Class: {entry.Name}<br>";
                    mainContent += $"Code: {entry.Code}<br><br>";
                    mainContent += "Attendance Details:<br>";

                    foreach (var attendance in attendanceResponse.Items)
                    {
                        var student = attendance.ClassStudent.Student;
                        mainContent += $"- {student.Name} ({student.Email}) - Reason: {attendance.Reason}<br>";
                    }

                    var body = await _mediator.Send(new GetMailTemplateQuery
                    {
                        title = title,
                        speech = null,
                        mainContent = mainContent,
                        sign = sign
                    });

                    var mailData = new SendMailCommand
                    {
                        To = to,
                        Subject = subject,
                        Body = body.Replace("{speech}", speech)
                    };
                    await _mediator.Send(mailData);
                }
            }
        }
    }
}
