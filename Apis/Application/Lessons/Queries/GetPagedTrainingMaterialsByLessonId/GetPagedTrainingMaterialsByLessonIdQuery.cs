using Application.Commons;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Lessons.Queries.GetPagedTrainingMaterialsByLessonId;

public record GetPagedTrainingMaterialsByLessonIdQuery : IRequest<Pagination<TrainingMaterialDto>>
{
    public int LessonId { get; set; }
    public string? Keyword { get; init; }
    public int PageIndex { get; init; } = 0;
    public int PageSize { get; init; } = 10;
    public SortType SortType { get; init; } = SortType.Ascending;
}

public class GetPagedTrainingMaterialsByLessonIdQueryHandler : IRequestHandler<GetPagedTrainingMaterialsByLessonIdQuery, Pagination<TrainingMaterialDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedTrainingMaterialsByLessonIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<TrainingMaterialDto>> Handle(
        GetPagedTrainingMaterialsByLessonIdQuery request,
        CancellationToken cancellationToken)
    {
        var pagedItems = await _unitOfWork.TrainingMaterialRepository.GetAsync<int>(
            filter: x =>
                x.UnitLessonId == request.LessonId &&
                x.FileName.Contains(request.Keyword ?? ""),
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            sortType: SortType.Ascending,
            keySelectorForSort: x => x.Id);
        var result = _mapper.Map<Pagination<TrainingMaterialDto>>(pagedItems);
        return result;
    }
}

public class TrainingMaterialDto
{
#pragma warning disable
    public int Id { get; init; }
    public int UnitLessonId { get; init; }
    public int TestAssessmentId { get; init; }
    public string FileName { get; init; }
    public string FilePath { get; init; }
    public long FileSize { get; init; }
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TrainingMaterial, TrainingMaterialDto>();
    }
}
