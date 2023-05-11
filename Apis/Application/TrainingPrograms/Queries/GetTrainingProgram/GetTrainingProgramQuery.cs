using Application.Commons;
using Application.TrainingPrograms.DTOs;
using AutoMapper;
using MediatR;

namespace Application.TrainingPrograms.Queries.GetTrainingProgram
{
    public record GetTrainingProgramQuery(int pageIndex = 0, int pageSize = 10) : IRequest<Pagination<TrainingProgramDTO>>;
    public class GetTrainingProgramHandler : IRequestHandler<GetTrainingProgramQuery, Pagination<TrainingProgramDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTrainingProgramHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<TrainingProgramDTO>> Handle(GetTrainingProgramQuery request, CancellationToken cancellationToken)
        {
            var trainingProgram = await _unitOfWork.TrainingProgramRepository.ToPagination(request.pageIndex, request.pageSize);
            var result = _mapper.Map<Pagination<TrainingProgramDTO>>(trainingProgram);
            return result;
        }
    }
}