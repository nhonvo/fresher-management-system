using Application.Commons;
using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Aggregate.AppResult;
using MediatR;
namespace Application.Syllabuses.Queries.GetSyllabusName
{
    public record GetSyllabusNameQuery(string? name, int pageIndex = 0, int pageSize = 10) : IRequest<ApiResult<Pagination<SyllabusDTO>>>;
    public class GetSyllabusNameHandler : IRequestHandler<GetSyllabusNameQuery, ApiResult<Pagination<SyllabusDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSyllabusNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult<Pagination<SyllabusDTO>>> Handle(GetSyllabusNameQuery request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.SyllabusRepository.GetAsync(
                filter: x => x.Name == request.name,
                // && x.IsActive == true,
                pageIndex: request.pageIndex,
                pageSize: request.pageSize);

            var result = _mapper.Map<Pagination<SyllabusDTO>>(syllabus);
            if (result.Items != null)
                return new ApiErrorResult<Pagination<SyllabusDTO>>("Not found syllabus");

            return new ApiSuccessResult<Pagination<SyllabusDTO>>(result);
        }
    }
}