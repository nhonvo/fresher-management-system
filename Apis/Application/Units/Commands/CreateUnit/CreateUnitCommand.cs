using Application.Interfaces;
using Application.Units.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Units.Commands.CreateUnit
{
    public record CreateUnitCommand : IRequest<UnitHasIdDTO>
    {
        public string Name { get; init; }
        public int SyllabusSession { get; init; }
        public int UnitNumber { get; init; }
        public int SyllabusId { get; init; }
    }

    public class CreateUnitHandler : IRequestHandler<CreateUnitCommand, UnitHasIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public CreateUnitHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
        }

        public async Task<UnitHasIdDTO> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = _mapper.Map<Domain.Entities.Unit>(request);
            unit.CreatedBy = _claimService.CurrentUserId;
            unit.CreationDate = _currentTime.GetCurrentTime();
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.UnitRepository.AddAsync(unit);
            });
            var result = _mapper.Map<UnitHasIdDTO>(unit);

            return result;


        }
    }
}
