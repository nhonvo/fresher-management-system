using Application.Calenders.DTO;
using Application.Commons;
using AutoMapper;
using MediatR;

namespace Application.Calenders.Queries.GetPagedCalenders;

public record GetPagedCalendersQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<CalenderDTO>>;

public class GetPagedCalendersHandler : IRequestHandler<GetPagedCalendersQuery, Pagination<CalenderDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedCalendersHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<CalenderDTO>> Handle(GetPagedCalendersQuery request, CancellationToken cancellationToken)
    {
        var unit = await _unitOfWork.CalenderRepository.GetAsync(pageIndex: request.PageIndex, pageSize: request.PageSize);
        var result = _mapper.Map<Pagination<CalenderDTO>>(unit);
        return result;
    }
}
