using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.TrainingPrograms.Commands.AddOneSyllabusToTrainingProgram
{
    public record AddOneSyllabusToTrainingProgramCommand(int syllabusId, int trainingProgramId) : IRequest<bool>;
    public class AddOneSyllabusToTrainingProgramHandler : IRequestHandler<AddOneSyllabusToTrainingProgramCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public AddOneSyllabusToTrainingProgramHandler(
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
        public async Task<bool> Handle(AddOneSyllabusToTrainingProgramCommand request, CancellationToken cancellationToken)
        {
            var syllabusExist = await _unitOfWork.SyllabusRepository.AnyAsync(x => x.Id == request.syllabusId);
            if (syllabusExist is false)
                throw new NotFoundException("syllabus is not exist!!");
            var trainingProgram = await _unitOfWork.TrainingProgramRepository.FirstOrDefaultAsync(x => x.Id == request.trainingProgramId);
            if (trainingProgram is null)
                throw new NotFoundException("training program is not exist!!");
            foreach (var item in trainingProgram.ProgramSyllabus)
            {
                if (item.SyllabusId == request.syllabusId && item.TrainingProgramId == request.trainingProgramId)
                {
                    throw new TransactionException("This Training Program already has this Syllabus");
                }
            }
            trainingProgram.ProgramSyllabus.Add(new ProgramSyllabus
            {
                SyllabusId = request.syllabusId,
                TrainingProgramId = request.trainingProgramId
            });
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}