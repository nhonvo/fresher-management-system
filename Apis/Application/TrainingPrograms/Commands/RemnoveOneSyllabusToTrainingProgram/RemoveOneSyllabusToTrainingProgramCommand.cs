using Application.Common.Exceptions;
using Application.Interfaces;
using Application.TrainingPrograms.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.TrainingPrograms.Commands.RemoveOneSyllabusToTrainingProgram
{
    public record RemoveOneSyllabusToTrainingProgramCommand(int syllabusId, int trainingProgramId) : IRequest<bool>;
    public class RemoveOneSyllabusToTrainingProgramHandler : IRequestHandler<RemoveOneSyllabusToTrainingProgramCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public RemoveOneSyllabusToTrainingProgramHandler(
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
        public async Task<bool> Handle(RemoveOneSyllabusToTrainingProgramCommand request, CancellationToken cancellationToken)
        {
            var syllabusExist = await _unitOfWork.SyllabusRepository.AnyAsync(x => x.Id == request.syllabusId);
            if (syllabusExist)
            {
                throw new NotFoundException("syllabus is not exist!!");
            }
            var trainingProgram = await _unitOfWork.TrainingProgramRepository.AnyAsync(x => x.Id == request.trainingProgramId);
            if (trainingProgram)
            {
                throw new NotFoundException("training program is not exist!!");
            }
            var programSyllabus = await _unitOfWork.ProgramSyllabusRepository.FirstOrDefaultAsync(x => x.SyllabusId == request.syllabusId && x.TrainingProgramId == request.trainingProgramId);
            if (programSyllabus == null)
            {
                throw new NotFoundException("training program has not syllabus");
            }
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ProgramSyllabusRepository.Delete(programSyllabus);
            });
            return true;
        }
    }
}