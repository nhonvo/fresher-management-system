using System.Transactions;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Syllabuses.Commands.AddOneUnitToSyllabus
{
    public record AddOneUnitToSyllabusCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public int SyllabusSession { get; set; }
        public int UnitNumber { get; set; }
        public int SyllabusId { get; set; }
    }

    public class AddOneUnitToSyllabusHandler : IRequestHandler<AddOneUnitToSyllabusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public AddOneUnitToSyllabusHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime;
            _claimService = claimService;
        }

        public async Task<bool> Handle(AddOneUnitToSyllabusCommand request, CancellationToken cancellationToken)
        {
            var syllabuses = await _unitOfWork.SyllabusRepository.AnyAsync(x => x.Id == request.SyllabusId);
            if (syllabuses is false) 
                throw new TransactionException("Syllabus not exist");
            var unit = _mapper.Map<Domain.Entities.Unit>(request);
            unit.CreationDate = _currentTime.GetCurrentTime();
            unit.CreatedBy = _claimService.CurrentUserId;
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.UnitRepository.AddAsync(unit);
            });
            return true;
        }
    }
}