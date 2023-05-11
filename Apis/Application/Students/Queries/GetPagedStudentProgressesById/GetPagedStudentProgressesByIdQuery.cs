using Application.Commons;
using Application.Students.DTO;
using AutoMapper;
using MediatR;

namespace Application.StudentProgresses.Queries.GetPagedStudentProgressesById;

public record GetPagedStudentProgressesByIdQuery : IRequest<Pagination<StudentProgressDTO>>
{
    public int Id { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetPagedStudentProgressesByIdHandler : IRequestHandler<GetPagedStudentProgressesByIdQuery, Pagination<StudentProgressDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPagedStudentProgressesByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<StudentProgressDTO>> Handle(GetPagedStudentProgressesByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ClassStudentRepository.GetPagedStudentProgressesById(
            id: request.Id,
            pageNumber: request.PageIndex,
            pageSize: request.PageSize);
        return result;
    }
}
