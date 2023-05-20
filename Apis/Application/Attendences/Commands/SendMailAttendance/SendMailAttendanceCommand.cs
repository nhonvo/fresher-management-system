using Application.Interfaces;
using AutoMapper;
using MediatR;
using Application.Attendances.Queries.GetAttendanceEachClass;

namespace Application.Attendances.Commands.SendMailAttendance
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

            // Group absentees by trainer
            var absenteesByTrainer = trainingClass.Items
                .GroupBy(a => a.ClassStudent.TrainingClass)
                .ToDictionary(g => g.Key, g => g.Select(a => a.ClassStudent.TrainingClass.ClassAdmins.Select(x => x.Admin)).ToList());

            // foreach (var entry in absenteesByTrainer)
            // {
            //     var admin = entry.Key;
            //     var trainees = entry.Value;

            //     var to = new List<string>();
            //     var title = $"Attendance Report for {DateTime.Today:d}";
            //     var speech = $"Dear {admin},<br><br>The following trainees were absent in your class today ({DateTime.Today:d}):<br>";
            //     var mainContent = "Hello";

            //     foreach (var trainee in trainees)
            //     {
            //         speech += $"- {trainee.FullName} ({trainee.Email})<br>";
            //         to.Add(trainee.Email);
            //     }

            //     to.AddRange(admin.Email);
            //     var sign = "Admin";
            //     var body = await _mediator.Send(new GetMailTemplateQuery
            //     {
            //         title = title,
            //         speech = speech,
            //         mainContent = mainContent,
            //         sign = sign
            //     });
            //     var subject = $"Attendance Report for {DateTime.Today:d}";
            //     // Send email
            //     var mailData = new SendMailCommand
            //     {
            //         To = to,
            //         Subject = subject,
            //         Body = body
            //     };
            //     await _mediator.Send(mailData);
            // }


        }
    }
}
