using Application.Commons;
using Application.Syllabuses.DTOs;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Syllabuses.Queries.GetSyllabus
{
    public record GetSyllabusQuery(
        string? keyword = null,
        int PageIndex = 0,
        int PageSize = 10,
        SortType sortType = SortType.Ascending) : IRequest<Pagination<SyllabusRelated>>;

    public class GetSyllabusHandler : IRequestHandler<GetSyllabusQuery, Pagination<SyllabusRelated>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSyllabusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<SyllabusRelated>> Handle(GetSyllabusQuery request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.SyllabusRepository.GetAsync<int>(
                filter: x => x.Name.Contains(request.keyword ?? "")
                             || x.Code.Contains(request.keyword ?? "")
                             || x.Id.ToString().Contains(request.keyword ?? ""),
                include: x => x.Include(x => x.CreateByUser)
                               .Include(x => x.ModificationByUser)
                               .Include(x => x.Units)
                               .ThenInclude(x => x.Lessons)
                               .ThenInclude(x => x.TrainingMaterials),
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                sortType: SortType.Ascending,
                keySelectorForSort: x => x.Id);
            var result = _mapper.Map<Pagination<SyllabusRelated>>(syllabus);
            return result;
        }
    }
}