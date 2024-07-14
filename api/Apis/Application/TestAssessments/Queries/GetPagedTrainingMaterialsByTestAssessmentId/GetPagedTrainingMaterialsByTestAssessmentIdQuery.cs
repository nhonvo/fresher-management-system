using Application.Commons;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.TestAssessments.Queries.GetPagedTrainingMaterialsByTestAssessmentId;

public record GetPagedTrainingMaterialsByTestAssessmentIdQuery : IRequest<Pagination<TrainingMaterialDto>>
{
    public int TestAssessmentId { get; set; }
    public string? Keyword { get; init; }
    public int PageIndex { get; init; } = 0;
    public int PageSize { get; init; } = 10;
    public SortType SortType { get; init; } = SortType.Ascending;
}

public class GetPagedTrainingMaterialsByTestAssessmentIdQueryHandler : IRequestHandler<GetPagedTrainingMaterialsByTestAssessmentIdQuery, Pagination<TrainingMaterialDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedTrainingMaterialsByTestAssessmentIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<TrainingMaterialDto>> Handle(
        GetPagedTrainingMaterialsByTestAssessmentIdQuery request,
        CancellationToken cancellationToken)
    {
        var pagedItems = await _unitOfWork.TrainingMaterialRepository.GetAsync<int>(
            filter: x =>
                x.TestAssessmentId == request.TestAssessmentId &&
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
