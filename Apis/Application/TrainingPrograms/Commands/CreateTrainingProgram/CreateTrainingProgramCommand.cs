using Application.Interfaces;
using Application.TrainingPrograms.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.TrainingPrograms.Commands.CreateTrainingProgram
{
    public record CreateTrainingProgramCommand : IRequest<TrainingProgramDTO>
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public TrainingProgramStatus Status { get; set; }

        //Navigation properties
        // public int? ParentId { get; set; }
        // public TrainingProgram? Parent { get; set; }
        // public int? TrainingClassId { get; set; }
        // public TrainingClass? TrainingClass { get; set; }
        // public ICollection<ProgramSyllabus> ProgramSyllabus { get; set; }
        // public ICollection<TestAssessment> TestAssessments { get; set; }
        public DateTime CreationDate { get; set; }
        public int? CreatedBy { get; set; }
        // public User? CreateByUser { get; set; }
    }
    public class CreateTrainingProgramCommandHandler : IRequestHandler<CreateTrainingProgramCommand, TrainingProgramDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public CreateTrainingProgramCommandHandler(
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
        public async Task<TrainingProgramDTO> Handle(CreateTrainingProgramCommand request, CancellationToken cancellationToken)
        {
            var trainingProgram = _mapper.Map<TrainingProgram>(request);
            trainingProgram.CreatedBy = _claimService.CurrentUserId;
            trainingProgram.CreationDate = _currentTime.GetCurrentTime();
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.TrainingProgramRepository.AddAsync(trainingProgram);
            });
            var result = _mapper.Map<TrainingProgramDTO>(trainingProgram);
            return result;
        }
    }
}