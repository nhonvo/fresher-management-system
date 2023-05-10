using Application.Attendances.DTOs;
using Application.Common.Exceptions;
using Application.Commons;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Attendances.Queries.GetAttendanceByClass;

public record GetAttendanceByClassQuery(int id, int pageIndex = 0, int pageSize = 10) : IRequest<Pagination<AttendanceDTO>> { };
public class GetAttendanceByClassQueryHandler : IRequestHandler<GetAttendanceByClassQuery, Pagination<AttendanceDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJWTService _jwtService;
    private readonly IMapper _mapper;

    public GetAttendanceByClassQueryHandler(IUnitOfWork unitOfWork, IJWTService jwtService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task<Pagination<AttendanceDTO>> Handle(GetAttendanceByClassQuery request, CancellationToken cancellationToken)
    {
        var isExist = await _unitOfWork.ClassRepository.FirstOrdDefaultAsync(x => x.Id == request.id);
        if (isExist == null)
            throw new NotFoundException("class not found");

        var attendances = await _unitOfWork.AttendanceRepository.GetAsync(
            filter: x => x.ClassUser.Id == request.id,
            include: x => x.Include(x => x.ClassUser),
            pageIndex: request.pageIndex,
            pageSize: request.pageSize
        );

        var result = _mapper.Map<Pagination<AttendanceDTO>>(attendances);
        return result;
    }
}
