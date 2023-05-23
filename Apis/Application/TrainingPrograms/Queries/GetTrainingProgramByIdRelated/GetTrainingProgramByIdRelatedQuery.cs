using Application.TrainingPrograms.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TrainingPrograms.Queries.GetTrainingProgramByIdRelated
{
    public record GetTrainingProgramByIdRelatedQuery(int id) : IRequest<TrainingProgramHasIdRelated>;
    public class GetTrainingProgramByIdRelatedHandler : IRequestHandler<GetTrainingProgramByIdRelatedQuery, TrainingProgramHasIdRelated>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTrainingProgramByIdRelatedHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TrainingProgramHasIdRelated> Handle(GetTrainingProgramByIdRelatedQuery request, CancellationToken cancellationToken)
        {
            var trainingProgram = await _unitOfWork.TrainingProgramRepository.FirstOrDefaultAsync(
                filter: x => x.Id == request.id,
                include: x => x.Include(x => x.CreateByUser)
                            .Include(x => x.ModificationByUser)
                            .Include(x => x.ProgramSyllabus)
                                .ThenInclude(x => x.Syllabus)
                                .ThenInclude(x => x.Units)
                                .ThenInclude(x => x.Lessons)
                                .ThenInclude(x => x.TrainingMaterials));
            var result = _mapper.Map<TrainingProgramHasIdRelated>(trainingProgram);
            return result;
        }
    }
}