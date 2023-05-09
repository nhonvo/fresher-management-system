using Application.Common.Exceptions;
using Application.Commons;
using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Aggregate.AppResult;
using MediatR;


namespace Application.Syllabuses.Queries.GetSyllabus
{
    public record GetSyllabusQuery(int PageIndex = 0, int PageSize = 10) : IRequest<ApiResult<Pagination<SyllabusDTO>>>;

    public class GetSyllabusHandler : IRequestHandler<GetSyllabusQuery, ApiResult<Pagination<SyllabusDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSyllabusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResult<Pagination<SyllabusDTO>>> Handle(GetSyllabusQuery request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.SyllabusRepository.ToPagination(request.PageIndex, request.PageSize);
            var result = _mapper.Map<Pagination<SyllabusDTO>>(syllabus);
            return new ApiSuccessResult<Pagination<SyllabusDTO>>(result);
        }
    }
}