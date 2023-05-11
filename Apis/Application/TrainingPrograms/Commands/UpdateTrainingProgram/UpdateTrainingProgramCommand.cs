using Application.Common.Exceptions;
using Application.Interfaces;
using Application.TrainingPrograms.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.TrainingPrograms.Commands.UpdateTrainingProgram
{
    public record UpdateTrainingProgramCommand : IRequest<TrainingProgramDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public TrainingProgramStatus Status { get; set; }
    }
    public class UpdateTrainingProgramCommandHandler : IRequestHandler<UpdateTrainingProgramCommand, TrainingProgramDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public UpdateTrainingProgramCommandHandler(
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
        public async Task<TrainingProgramDTO> Handle(UpdateTrainingProgramCommand request, CancellationToken cancellationToken)
        {
            var trainingProgram = await _unitOfWork.TrainingProgramRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (trainingProgram == null)
                throw new NotFoundException("TrainingProgram not found");
            trainingProgram = _mapper.Map<TrainingProgram>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.TrainingProgramRepository.Update(trainingProgram);
            });
            var result = _mapper.Map<TrainingProgramDTO>(trainingProgram);
            return result ?? throw new NotFoundException("Can not update trainingProgram");

        }
    }
}