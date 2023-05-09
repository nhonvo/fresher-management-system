using Application.Commons;
using Application.TestAssessments.DTO;
using AutoMapper;
using Domain.Aggregate.AppResult;
using MediatR;

namespace Application.TestAssessments.Queries.GetPagedTestAssessments;

public record GetPagedTestAssessmentsQuery(int PageIndex = 0, int PageSize = 10) : IRequest<ApiResult<Pagination<TestAssessmentDTO>>>;

public class GetPagedTestAssessmentsHandler : IRequestHandler<GetPagedTestAssessmentsQuery, ApiResult<Pagination<TestAssessmentDTO>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedTestAssessmentsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ApiResult<Pagination<TestAssessmentDTO>>> Handle(GetPagedTestAssessmentsQuery request, CancellationToken cancellationToken)
    {
        var syllabus = await _unitOfWork.TestAssessmentRepository.ToPagination(request.PageIndex, request.PageSize);
        var result = _mapper.Map<Pagination<TestAssessmentDTO>>(syllabus);
        return new ApiSuccessResult<Pagination<TestAssessmentDTO>>(result);
    }
}
