using Application.Common.Exceptions;
using Application.TestAssessments.DTO;
using AutoMapper;
using MediatR;

namespace Application.TestAssessments.Queries.GetTestAssessmentById;

public record GetTestAssessmentByIdQuery(int id) : IRequest<TestAssessmentDTO>;

public class GetTestAssessmentByIdHandler : IRequestHandler<GetTestAssessmentByIdQuery, TestAssessmentDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTestAssessmentByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<TestAssessmentDTO> Handle(GetTestAssessmentByIdQuery request, CancellationToken cancellationToken)
    {
        var syllabus = await _unitOfWork.TestAssessmentRepository.GetByIdAsync(request.id);
        var result = _mapper.Map<TestAssessmentDTO>(syllabus);
        return result ?? throw new NotFoundException("TestAssessment not found", request.id);

    }
}
