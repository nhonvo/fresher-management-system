using Application.Calenders.DTO;
using Application.Common.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Calenders.Queries.GetCalenderById;

public record GetCalenderByIdQuery(int id) : IRequest<CalenderDTO>;

public class GetCalenderByIdHandler : IRequestHandler<GetCalenderByIdQuery, CalenderDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCalenderByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CalenderDTO> Handle(GetCalenderByIdQuery request, CancellationToken cancellationToken)
    {
        var unit = await _unitOfWork.CalenderRepository.GetByIdAsync(request.id);
        var result = _mapper.Map<CalenderDTO>(unit);
        return result ?? throw new NotFoundException("Calender not found", request.id);
    }
}
