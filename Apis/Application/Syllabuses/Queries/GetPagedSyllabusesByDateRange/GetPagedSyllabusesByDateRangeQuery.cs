using Application.Commons;
using Application.Syllabuses.DTO;
using AutoMapper;
using MediatR;

namespace Application.Syllabuses.Queries.GetPagedSyllabusesByDateRange;

public record GetPagedSyllabusesByDateRangeQuery : IRequest<Pagination<SyllabusDTO>>
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
}

public class GetPagedSyllabusesByDateRangeHandler : IRequestHandler<GetPagedSyllabusesByDateRangeQuery, Pagination<SyllabusDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedSyllabusesByDateRangeHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<SyllabusDTO>> Handle(GetPagedSyllabusesByDateRangeQuery request, CancellationToken cancellationToken)
    {
        var syllabus = await _unitOfWork.SyllabusRepository.ToPagination(
            // filter: s => s.CreatedOn.Date >= request.FromDate.Date && s.CreatedOn.Date <= request.ToDate.Date,
            pageIndex: request.PageIndex,
            pageSize: request.PageSize);
        var result = _mapper.Map<Pagination<SyllabusDTO>>(syllabus);
        return result;
    }
}
