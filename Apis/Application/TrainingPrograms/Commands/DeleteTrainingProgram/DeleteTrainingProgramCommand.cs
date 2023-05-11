using Application.Common.Exceptions;
using Application.Interfaces;
using Application.TrainingPrograms.DTOs;
using AutoMapper;
using MediatR;

namespace Application.TrainingPrograms.Commands.DeleteTrainingProgram
{
    public record DeleteTrainingProgramCommand(int id) : IRequest<TrainingProgramDTO>;
    public class DeleteTrainingProgramCommandHandler : IRequestHandler<DeleteTrainingProgramCommand, TrainingProgramDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public DeleteTrainingProgramCommandHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IClaimService claimService,
            ICurrentTime currentTime)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimService = claimService;
            _currentTime = currentTime;
        }
        public async Task<TrainingProgramDTO> Handle(DeleteTrainingProgramCommand request, CancellationToken cancellationToken)
        {

            var trainingProgram = await _unitOfWork.TrainingProgramRepository.GetByIdAsync(request.id);
            if (trainingProgram == null)
                throw new NotFoundException("TrainingProgram not found");
            trainingProgram.DeleteBy = _claimService.CurrentUserId;
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.TrainingProgramRepository.Delete(trainingProgram);
            });
            var result = _mapper.Map<TrainingProgramDTO>(trainingProgram);
            return result ?? throw new NotFoundException("Can not delete trainingProgram");

        }
    }
}