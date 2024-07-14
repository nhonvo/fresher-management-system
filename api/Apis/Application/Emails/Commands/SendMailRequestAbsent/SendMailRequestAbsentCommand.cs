﻿using Application.Attendances.Queries.GetAttendanceById;
using Application.Emails.Commands.SendMail;
using Application.Emails.Queries;
using Application.Interfaces;
using Application.Users.GetProfile.Queries;
using AutoMapper;
using MediatR;

namespace Application.Emails.Commands.SendMailRequestAbsent
{
    public record SendMailRequestAbsentCommand(int attendanceId) : IRequest;
    public class SendMailRequestAbsentHandler : IRequestHandler<SendMailRequestAbsentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        private readonly IMediator _mediator;

        public SendMailRequestAbsentHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
            _mediator = mediator;
        }

        public async Task Handle(SendMailRequestAbsentCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetProfileQuery());
            var attendance = await _mediator.Send(new GetAttendanceByIdQuery(request.attendanceId));

            var to = new List<string> { user.Email };

            var title = "Successfully request absence";
            var speech = "Successfully request absence";
            var mainContent =
            $@"
                <div>
                    <h2>Your class Information:</h2>
                    <ul>
                        <li><strong>Name:</strong> {user.Name}</li>
                        <li><strong>Email:</strong> {user.Email}</li>
                        <li><strong>Phone:</strong> {user.Phone}</li>
                    </ul>
                       <h2>Attendance:</h2>
                    <ul>
                        <li><strong>Reason:</strong> {attendance.Reason}</li>
                        <li><strong>Status:</strong> {attendance.AttendanceStatus}</li>
                        <li><strong>Day:</strong> {attendance.Day}</li>
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
            var subject = "Successfully request absence";
            var mailData = new SendMailCommand
            {
                To = to,
                Subject = subject,
                Body = body
            };
            await _mediator.Send(mailData, new CancellationToken());
        }
    }
}
