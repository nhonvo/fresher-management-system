using Application.Commons;
using Application.Calenders.DTO;
using AutoMapper;
using MediatR;

namespace Application.Calenders.Queries.GetPagedCalendersByTrainingClassId;

public record GetPagedCalendersByTrainingClassIdQuery : IRequest<Pagination<CalenderDTO>>
{
    public int TrainingClassId { get; init; }
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
};

public class GetPagedCalendersByTrainingClassIdHandler : IRequestHandler<GetPagedCalendersByTrainingClassIdQuery, Pagination<CalenderDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedCalendersByTrainingClassIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<CalenderDTO>> Handle(GetPagedCalendersByTrainingClassIdQuery request, CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.CalenderRepository.ToPagination(
            filter: x => x.TrainingClassId == request.TrainingClassId,
            pageIndex: request.PageIndex,
            pageSize: request.PageSize);
        var result = _mapper.Map<Pagination<CalenderDTO>>(items);
        return result;
    }
}
