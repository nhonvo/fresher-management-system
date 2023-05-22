using Application.Account.DTOs;
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
            // var TrainingClass = await _mediator.
            var to = new List<string> { user.Email };
            var title = "Welcome to FPT Software Academy";
            var speech = "Greetings,\nWelcome to FPT Software Academy and thank you for register account. This is your information:";
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
            var subject = "Welcome to FPT Software Academy";
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