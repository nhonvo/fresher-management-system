using Application.Commons;
using Application.TestAssessments.DTO;
using AutoMapper;
using MediatR;

namespace Application.TestAssessments.Queries.GetPagedTestAssessments;

public record GetPagedTestAssessmentsQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<TestAssessmentDTO>>;

public class GetPagedTestAssessmentsHandler : IRequestHandler<GetPagedTestAssessmentsQuery, Pagination<TestAssessmentDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedTestAssessmentsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Pagination<TestAssessmentDTO>> Handle(GetPagedTestAssessmentsQuery request, CancellationToken cancellationToken)
    {
        var syllabus = await _unitOfWork.TestAssessmentRepository.ToPagination(request.PageIndex, request.PageSize);
        var result = _mapper.Map<Pagination<TestAssessmentDTO>>(syllabus);
        return result;
    }
}
