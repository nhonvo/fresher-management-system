using Application.Commons;
using Application.TrainingPrograms.DTOs;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TrainingPrograms.Queries.GetTrainingProgramRelated
{
    public record GetTrainingProgramRelatedQuery(
        string? keyword,
        SortType sortType = SortType.Ascending,
        int pageIndex = 0,
        int pageSize = 10) : IRequest<Pagination<TrainingProgramHasIdRelated>>;
    public class GetTrainingProgramRelatedHandler : IRequestHandler<GetTrainingProgramRelatedQuery, Pagination<TrainingProgramHasIdRelated>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTrainingProgramRelatedHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<TrainingProgramHasIdRelated>> Handle(GetTrainingProgramRelatedQuery request, CancellationToken cancellationToken)
        {
            var trainingProgram = await _unitOfWork.TrainingProgramRepository.GetAsync<int>(
                 filter: x => x.Name.Contains(request.keyword ?? "")
                             || x.Id.ToString().Contains(request.keyword ?? ""),
                include: x => x.Include(x => x.CreateByUser)
                               .Include(x => x.ModificationByUser)
                               .Include(x => x.ProgramSyllabus)
                                    .ThenInclude(x => x.Syllabus)
                                    .ThenInclude(x => x.Units)
                                    .ThenInclude(x => x.Lessons)
                                    .ThenInclude(x => x.TrainingMaterials),
                pageIndex: request.pageIndex,
                pageSize: request.pageSize,
                sortType: SortType.Ascending,
                keySelectorForSort: x => x.Id);
            var result = _mapper.Map<Pagination<TrainingProgramHasIdRelated>>(trainingProgram);
            return result;
        }
    }
}