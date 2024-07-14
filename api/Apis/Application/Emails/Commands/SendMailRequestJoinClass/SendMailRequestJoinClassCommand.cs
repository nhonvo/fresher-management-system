using Application.Account.DTOs;
using Application.Class.Queries.GetClassById;
using Application.Emails.Commands.SendMail;
using Application.Emails.Queries;
using Application.Interfaces;
using Application.Users.GetProfile.Queries;
using MediatR;

namespace Application.Emails.Commands.SendMailRequestJoinClass
{
    public record SendMailRequestJoinClassCommand(int classId) : IRequest<bool>;
    public class SendMailRequestJoinClassHandler : IRequestHandler<SendMailRequestJoinClassCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IClaimService _claimService;

        public SendMailRequestJoinClassHandler(IMediator mediator, IClaimService claimService)
        {
            _mediator = mediator;
            _claimService = claimService;
        }

        public async Task<bool> Handle(SendMailRequestJoinClassCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetProfileQuery());
            var trainingClass = await _mediator.Send(new GetClassByIdQuery(request.classId));

            var to = new List<string> { user.Email };

            var title = "Successfully registered for the class";
            var speech = "Greetings,\nWelcome to FPT Software Academy, and thank you for registering for the class. Here are your registration details";
            var mainContent =
            $@"
                <div>
                    <h2>Your class Information:</h2>
                    <ul>
                        <li><strong>Name:</strong> {user.Name}</li>
                        <li><strong>Email:</strong> {user.Email}</li>
                        <li><strong>Phone:</strong> {user.Phone}</li>
                        <li><strong>Date of Birth:</strong> {user.DateOfBirth.ToShortDateString()}</li>
                        <li><strong>Role:</strong> {user.Role.ToString()}</li>
                    </ul>
                       <h2>Class Details:</h2>
                    <ul>
                        <li><strong>Class Name:</strong> {trainingClass.Name}</li>
                        <li><strong>Class Date:</strong> {trainingClass.TimeStart.ToShortDateString()}</li>
                        <li><strong>Class Time:</strong> {trainingClass.TimeStart.ToShortTimeString()} - {trainingClass.TimeEnd.ToShortTimeString()}</li>
                        <li><strong>Class Location:</strong> {trainingClass.Location}</li>
                    </ul>

                </div>
            ";

            var sign = "FPT Academy Team";
            var body = await _mediator.Send(new GetMailTemplateQuery
            {
                title = title,
                speech = speech,
                mainContent = mainContent,
                sign = sign
            });
            var subject = "Successfully registered for the class";
            var mailData = new SendMailCommand
            {
                To = to,
                Subject = subject,
                Body = body
            };
            return await _mediator.Send(mailData, new CancellationToken());
        }
    }
}