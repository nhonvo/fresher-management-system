using Application.ApproveRequests.DTOs;
using Application.ApproveRequests.GetApproveById;
using Application.Emails.Commands.SendMail;
using Application.Emails.Queries;
using MediatR;

namespace Application.ApproveRequests.Commands.SendMailApproveRequest
{
    public record SendMailApproveRequestCommand(int id) : IRequest<bool>;
    public class SendMailApproveRequestHandler : IRequestHandler<SendMailApproveRequestCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public SendMailApproveRequestHandler(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SendMailApproveRequestCommand request, CancellationToken cancellationToken)
        {
            var approve = await _mediator.Send(new GetApproveByIdQuery(request.id));
            var to = new List<string> { approve.Student.Email };
            var isApproved = approve.Status == Domain.Enums.StatusApprove.Approve;
            var title = isApproved ? "Congratulations! You have been approved to join the class" : "Sorry! You have not been approved to join the class";
            var speech = isApproved ? "Greetings,\nCongratulations! You have been approved to join the class at FPT Software Academy. This is your information:" : "Sorry! You have not been approved to join the class at FPT Software Academy. This is your information:";
            var mainContent = isApproved ? GetMainContent(approve) : "Sorry";
            var sign = "FPT Academy Team";

            var body = await _mediator.Send(new GetMailTemplateQuery
            {
                title = title,
                speech = speech,
                mainContent = mainContent,
                sign = sign
            });

            var subject = isApproved ? "Congratulations! You have been approved to join the class" : "Sorry! You have not been approved to join the class";
            var mailData = new SendMailCommand
            {
                To = to,
                Subject = subject,
                Body = body
            };

            return await _mediator.Send(mailData, new CancellationToken());
        }

        private string GetMainContent(ApproveRequestRelatedDTO approve)
        {
            var trainingClass = approve.TrainingClass;
            var mainContent = $@"
        <table>
            <tr>
                <th>Attribute</th>
                <th>Value</th>
            </tr>
            <tr>
                <td><b>Class Name:</b></td>
                <td>{trainingClass.Name}</td>
            </tr>
            <tr>
                <td><b>Class Code:</b></td>
                <td>{trainingClass.Code}</td>
            </tr>
            <tr>
                <td><b>Class Time Start:</b></td>
                <td>{trainingClass.ClassTimeStart}</td>
            </tr>
            <tr>
                <td><b>Class Time End:</b></td>
                <td>{trainingClass.ClassTimeEnd}</td>
            </tr>
            <tr>
                <td><b>Review On:</b></td>
                <td>{trainingClass.ReviewOn}</td>
            </tr>
            <tr>
                <td><b>Approve On:</b></td>
                <td>{trainingClass.ApproveOn}</td>
            </tr>
            <tr>
                <td><b>Number of Planned Attendees:</b></td>
                <td>{trainingClass.NumberAttendeePlanned}</td>
            </tr>
            <tr>
                <td><b>Number of Accepted Attendees:</b></td>
                <td>{trainingClass.NumberAttendeeAccepted}</td>
            </tr>
            <tr>
                <td><b>Number of Actual Attendees:</b></td>
                <td>{trainingClass.NumberAttendeeActual}</td>
            </tr>
            <tr>
                <td><b>Location:</b></td>
                <td>{trainingClass.Location}</td>
            </tr>
            <tr>
                <td><b>Status:</b></td>
                <td>{trainingClass.Status}</td>
            </tr>
        </table>
    ";
            return mainContent;
        }
    }
}