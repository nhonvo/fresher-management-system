using Application.Commons;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Class.Queries.GetClass;

public record GetClassQuery : IRequest<Pagination<ClassDto>>
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
};

public class GetClassHandler : IRequestHandler<GetClassQuery, Pagination<ClassDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetClassHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Pagination<ClassDto>> Handle(GetClassQuery request, CancellationToken cancellationToken)
    {
        var classes = await _unitOfWork.ClassRepository.GetAsync(
            include: x => x
                .Include(x => x.CreateBy)
                .Include(x => x.ReviewBy)
                .Include(x => x.ApproveBy)
                .Include(x => x.TrainingProgram)
                    .ThenInclude(x => x.ProgramSyllabus)
                    .ThenInclude(x => x.Syllabus)
                    .ThenInclude(x => x.Units)
                    .ThenInclude(x => x.Lessons)
                    .ThenInclude(x => x.TrainingMaterials)
                .Include(x => x.Calenders),
            pageIndex: request.PageIndex,
            pageSize: request.PageSize);

        var result = _mapper.Map<Pagination<ClassDto>>(classes);

        return result;
    }
}
