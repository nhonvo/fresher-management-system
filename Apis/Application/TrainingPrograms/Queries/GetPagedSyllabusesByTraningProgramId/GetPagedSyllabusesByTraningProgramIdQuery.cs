using Application.Commons;
using AutoMapper;
using MediatR;

namespace Application.TrainingPrograms.Queries.GetPagedSyllabusesByTraningProgramId;

public record GetPagedSyllabusesByTraningProgramIdQuery : IRequest<Pagination<SyllabusDTO>>
{
    public int TrainingProgramId { get; init; }
    public int PageIndex { get; init; } = 0;
    public int PageSize { get; init; } = 10;
}

public class GetPagedSyllabusesByTraningProgramIdHandler : IRequestHandler<GetPagedSyllabusesByTraningProgramIdQuery, Pagination<SyllabusDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedSyllabusesByTraningProgramIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<SyllabusDTO>> Handle(GetPagedSyllabusesByTraningProgramIdQuery request, CancellationToken cancellationToken)
    {
        var syllabus = await _unitOfWork.SyllabusRepository.ToPagination(
            filter: s => s.ProgramSyllabus.Where(x => x.TrainingProgramId == request.TrainingProgramId).Any(),
            pageIndex: request.PageIndex,
            pageSize: request.PageSize);
        var result = _mapper.Map<Pagination<SyllabusDTO>>(syllabus);
        return result;
    }
}
