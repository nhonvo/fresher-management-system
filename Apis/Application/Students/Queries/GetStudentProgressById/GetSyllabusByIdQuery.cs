using Application.Commons;
using Application.Students.DTO;
using AutoMapper;
using Domain.Aggregate.AppResult;
using MediatR;

namespace Application.StudentProgresses.Queries.GetStudentProgressById;

public record GetPagedStudentProgressesByIdQuery(int id) : IRequest<ApiResult<Pagination<StudentProgressDTO>>>;

public class GetPagedStudentProgressesByIdHandler : IRequestHandler<GetPagedStudentProgressesByIdQuery, ApiResult<Pagination<StudentProgressDTO>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedStudentProgressesByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResult<Pagination<StudentProgressDTO>>> Handle(GetPagedStudentProgressesByIdQuery request, CancellationToken cancellationToken)
    {
        // var StudentProgress = await _unitOfWork.StudentProgressRepository.GetByIdAsync(request.id);
        // var result = _mapper.Map<StudentProgressDTO>(StudentProgress);
        var x = new ApiResult<Pagination<StudentProgressDTO>>();
        return x;
    }
}
