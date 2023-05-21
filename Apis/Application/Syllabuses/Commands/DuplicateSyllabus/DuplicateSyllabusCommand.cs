using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Syllabuses.Commands.DuplicateSyllabus
{
    public record DuplicateSyllabusCommand(int id) : IRequest<SyllabusDTO>;
    public class DuplicateSyllabusHandler : IRequestHandler<DuplicateSyllabusCommand, SyllabusDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public DuplicateSyllabusHandler(
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

        public async Task<SyllabusDTO> Handle(DuplicateSyllabusCommand request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.SyllabusRepository.AnyAsync(x => x.Id == request.id);
            if (exist is false)
                throw new NotFoundException("Syllabus not found");
            var syllabus = await _unitOfWork.SyllabusRepository.FirstOrDefaultAsync(
                filter: x => x.Id == request.id,
                include: x => x.Include(x => x.Units)
                               .ThenInclude(x => x.Lessons)
                               .ThenInclude(x => x.TrainingMaterials));
            var duplicate = _mapper.Map<SyllabusDuplicate>(syllabus);
            var syllabusUpdate = _mapper.Map<Syllabus>(duplicate);
            syllabusUpdate.CreatedBy = _claimService.CurrentUserId;
            syllabusUpdate.CreationDate = _currentTime.GetCurrentTime();

            syllabusUpdate.Units.ToList().ForEach(x => x.CreatedBy = _claimService.CurrentUserId);
            syllabusUpdate.Units.ToList().ForEach(x => x.CreationDate = _currentTime.GetCurrentTime());

            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.SyllabusRepository.AddAsync(syllabusUpdate);
            });
            var result = _mapper.Map<SyllabusDTO>(syllabusUpdate);
            return result;
        }
    }
}