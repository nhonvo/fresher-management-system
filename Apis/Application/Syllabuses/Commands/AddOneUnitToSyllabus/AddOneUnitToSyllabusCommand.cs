using Application.Interfaces;
using Application.Syllabuses.DTOs;
using AutoMapper;
using MediatR;
using System.Transactions;

namespace Application.Syllabuses.Commands.AddOneUnitToSyllabus
{
    public record AddOneUnitToSyllabusCommand : IRequest<AddOneUnitToSyllabusResponse>
    {
        public string Name { get; init; }
        public int SyllabusSession { get; init; }
        public int UnitNumber { get; init; }
        public int SyllabusId { get; init; }
    }

    public class AddOneUnitToSyllabusHandler : IRequestHandler<AddOneUnitToSyllabusCommand, AddOneUnitToSyllabusResponse>
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

        public async Task<AddOneUnitToSyllabusResponse> Handle(AddOneUnitToSyllabusCommand request, CancellationToken cancellationToken)
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
            var result = _mapper.Map<AddOneUnitToSyllabusResponse>(unit);
            return result;
        }
    }
}