using Application.Account.DTOs;
using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Enums.AttendanceEnums;
using MediatR;

namespace Application.Account.Commands.TakeAttendance;

public record TakeAttendanceCommand : IRequest<AccountDTO>
{
    public int ClassUserID { get; set; }
    public AttendanceStatus? AttendanceStatus { get; set; }
    public string Reason { get; set; }
}
public class TakeAttendanceCommandHandler : IRequestHandler<TakeAttendanceCommand, AccountDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJWTService _jwtService;
    private readonly IMapper _mapper;

    public TakeAttendanceCommandHandler(IUnitOfWork unitOfWork, IJWTService jwtService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public Task<AccountDTO> Handle(TakeAttendanceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
