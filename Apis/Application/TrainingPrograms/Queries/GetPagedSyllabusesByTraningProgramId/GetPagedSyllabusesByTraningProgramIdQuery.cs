using Application.Commons;
using Application.Syllabuses.DTOs;
using Application.TrainingPrograms.DTOs;
using AutoMapper;
using MediatR;

namespace Application.TrainingPrograms.Queries.GetPagedSyllabusesByTrainingProgramId;

public record GetPagedSyllabusesByTrainingProgramIdQuery : IRequest<Pagination<SyllabusRelated>>
{
    public int TrainingProgramId { get; init; }
    public int PageIndex { get; init; } = 0;
    public int PageSize { get; init; } = 10;
}

public class GetPagedSyllabusesByTrainingProgramIdHandler : IRequestHandler<GetPagedSyllabusesByTrainingProgramIdQuery, Pagination<SyllabusRelated>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedSyllabusesByTrainingProgramIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<SyllabusRelated>> Handle(GetPagedSyllabusesByTrainingProgramIdQuery request, CancellationToken cancellationToken)
    {
        var syllabus = await _unitOfWork.SyllabusRepository.ToPagination(
            filter: s => s.ProgramSyllabus.Where(x => x.TrainingProgramId == request.TrainingProgramId).Any(),
            pageIndex: request.PageIndex,
            pageSize: request.PageSize);
        var result = _mapper.Map<Pagination<SyllabusRelated>>(syllabus);
        return result;
    }
}
