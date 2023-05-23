using Application.Emails.Commands.SendMail;
using Application.Emails.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Emails.Commands.SendMailUserRegister;

public record SendMailUserRegisterCommand(AccountDto user) : IRequest<bool>;
public class SendMailUserRegisterHandler : IRequestHandler<SendMailUserRegisterCommand, bool>
{
    private readonly IMediator _mediator;

    public SendMailUserRegisterHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<bool> Handle(SendMailUserRegisterCommand request, CancellationToken cancellationToken)
    {
        var to = new List<string> { request.user.Email };
        var title = "Welcome to FPT Software Academy";
        var speech = "Greetings,\nWelcome to FPT Software Academy and thank you for register account. This is your information:";
        var mainContent =
        $@"
                <div>
                    <h2>Your Information:</h2>
                    <ul>
                        <li><strong>Name:</strong> {request.user.Name}</li>
                        <li><strong>Email:</strong> {request.user.Email}</li>
                        <li><strong>Phone:</strong> {request.user.Phone}</li>
                        <li><strong>Date of Birth:</strong> {request.user.DateOfBirth.ToShortDateString()}</li>
                        <li><strong>Role:</strong> {request.user.Role.ToString()}</li>
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

public record AccountDto
{
#pragma warning disable
    public int Id { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public UserRoleType Role { get; set; }
    public UserStatus Status { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime? ExpireDay { get; set; }
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, AccountDto>();
    }
}
