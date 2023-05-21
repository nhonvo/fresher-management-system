using Application.Account.DTOs;
using Application.Emails.Queries;
using MediatR;

namespace Application.Emails.Commands.SendMail
{
    public record SendMailCreateUserCommand(AccountDTO userinfo) : IRequest<bool>
    {

    }
    public class SendMailCreateUserHandler : IRequestHandler<SendMailCreateUserCommand, bool>
    {
        private readonly IMediator _mediator;

        public SendMailCreateUserHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(SendMailCreateUserCommand request, CancellationToken cancellationToken)
        {
            var to = new List<string> { request.userinfo.Email };
            var title = "Welcome to FPT Software Academy";
            var speech = "Greetings,\nWelcome to FPT Software Academy and thank you for register account. This is your information:";
            var mainContent =
            $@"
                <div>
                    <h2>Your Information:</h2>
                    <ul>
                        <li><strong>Name:</strong> {request.userinfo.Name}</li>
                        <li><strong>Email:</strong> {request.userinfo.Email}</li>
                        <li><strong>Phone:</strong> {request.userinfo.Phone}</li>
                        <li><strong>Date of Birth:</strong> {request.userinfo.DateOfBirth.ToShortDateString()}</li>
                        <li><strong>Role:</strong> {request.userinfo.Role.ToString()}</li>
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