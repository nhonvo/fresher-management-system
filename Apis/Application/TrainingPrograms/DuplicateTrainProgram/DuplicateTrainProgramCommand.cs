using Application.Common.Exceptions;
using Application.Interfaces;
using Application.TrainingPrograms.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TrainingPrograms.Commands.DuplicateTrainProgram
{
    public record DuplicateTrainProgramCommand(int id) : IRequest<TrainingProgramRelated>;
    public class DuplicateTrainProgramHandler : IRequestHandler<DuplicateTrainProgramCommand, TrainingProgramRelated>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public DuplicateTrainProgramHandler(
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
        public async Task<TrainingProgramRelated> Handle(DuplicateTrainProgramCommand request, CancellationToken cancellationToken)
        {

            var exist = await _unitOfWork.TrainingProgramRepository.AnyAsync(x => x.Id == request.id);
            if (exist is false)
            {
                throw new NotFoundException("Training program not found");
            }
            var trainingProgram = await _unitOfWork.TrainingProgramRepository.FirstOrDefaultAsync(
                filter: x => x.Id == request.id,
                include: x => x.Include(x => x.ProgramSyllabus)
                               .ThenInclude(x => x.Syllabus)
                               .ThenInclude(x => x.Units)
                               .ThenInclude(x => x.Lessons)
                               .ThenInclude(x => x.TrainingMaterials)
            );
            var duplicate = _mapper.Map<TrainingProgramDuplicate>(trainingProgram);
            var trainingProgramUpdate = _mapper.Map<TrainingProgram>(duplicate);
            // update time and current user
            trainingProgramUpdate.CreatedBy = _claimService.CurrentUserId;
            trainingProgramUpdate.CreationDate = _currentTime.GetCurrentTime();
            trainingProgramUpdate.ParentId = request.id;

            trainingProgramUpdate.ProgramSyllabus.ToList()
                                                 .ForEach(item => item.Syllabus.CreatedBy = _claimService.CurrentUserId);
            trainingProgramUpdate.ProgramSyllabus.ToList()
                                                 .ForEach(item => item.Syllabus.CreationDate = _currentTime.GetCurrentTime());

            trainingProgramUpdate.ProgramSyllabus.ToList()
                                                 .ForEach(item => item.Syllabus.Units.ToList()
                                                                                     .ForEach(unit => unit.CreatedBy = _claimService.CurrentUserId));
            trainingProgramUpdate.ProgramSyllabus.ToList()
                                                 .ForEach(item => item.Syllabus.Units.ToList()
                                                                                     .ForEach(unit => unit.CreationDate = _currentTime.GetCurrentTime()));

            // trainingProgramUpdate.ProgramSyllabus
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.TrainingProgramRepository.AddAsync(trainingProgramUpdate);
            });
            var result = _mapper.Map<TrainingProgramRelated>(trainingProgramUpdate);
            return result;
        }
    }
}
