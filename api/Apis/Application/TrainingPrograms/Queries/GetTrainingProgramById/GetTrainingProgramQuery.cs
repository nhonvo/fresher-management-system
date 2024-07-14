using Application.TrainingPrograms.DTOs;
using AutoMapper;
using MediatR;

namespace Application.TrainingPrograms.Queries.GetTrainingProgramById
{
    public record GetTrainingProgramByIdQuery(int id) : IRequest<TrainingProgramDTO>;
    public class GetTrainingProgramByIdHandler : IRequestHandler<GetTrainingProgramByIdQuery, TrainingProgramDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTrainingProgramByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TrainingProgramDTO> Handle(GetTrainingProgramByIdQuery request, CancellationToken cancellationToken)
        {
            var trainingProgram = await _unitOfWork.TrainingProgramRepository.GetByIdAsync(request.id);
            var result = _mapper.Map<TrainingProgramDTO>(trainingProgram);
            return result;
        }
    }
}