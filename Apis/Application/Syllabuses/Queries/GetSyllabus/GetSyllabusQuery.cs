using Application.Commons;
using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Enums;
using MediatR;


namespace Application.Syllabuses.Queries.GetSyllabus
{
    public record GetSyllabusQuery(
        string? keyword = null,
        int PageIndex = 0,
        int PageSize = 10,
        SortType sortType = SortType.Ascending) : IRequest<Pagination<SyllabusDTO>>;

    public class GetSyllabusHandler : IRequestHandler<GetSyllabusQuery, Pagination<SyllabusDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSyllabusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<SyllabusDTO>> Handle(GetSyllabusQuery request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.SyllabusRepository.GetAsync<string>(
                filter: x => x.Name.Contains(request.keyword ?? ""),
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                sortType: SortType.Ascending,
                keySelectorForSort: x => x.Name);
            var result = _mapper.Map<Pagination<SyllabusDTO>>(syllabus);
            return result;
        }
    }
}