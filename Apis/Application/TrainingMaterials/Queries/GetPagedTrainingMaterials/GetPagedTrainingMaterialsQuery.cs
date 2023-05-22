using Application.Commons;
using Application.TrainingMaterials.DTOs;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.TrainingMaterials.Queries.GetPagedTrainingMaterials;

public record GetPagedTrainingMaterialsQuery : IRequest<Pagination<TrainingMaterialDTO>>
{
    public string? Keyword = null;
    public int PageIndex = 0;
    public int PageSize = 10;
    public SortType SortType = SortType.Ascending;
}

public class GetPagedTrainingMaterialsQueryHandler : IRequestHandler<GetPagedTrainingMaterialsQuery, Pagination<TrainingMaterialDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedTrainingMaterialsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<TrainingMaterialDTO>> Handle(
        GetPagedTrainingMaterialsQuery request,
        CancellationToken cancellationToken)
    {
        var syllabus = await _unitOfWork.TrainingMaterialRepository.GetAsync<int>(
            filter: x => x.FileName.Contains(request.Keyword ?? ""),
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            sortType: SortType.Ascending,
            keySelectorForSort: x => x.Id);
        var result = _mapper.Map<Pagination<TrainingMaterialDTO>>(syllabus);
        return result;
    }
}
