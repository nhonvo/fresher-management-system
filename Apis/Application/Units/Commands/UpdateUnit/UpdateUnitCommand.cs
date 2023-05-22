using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Units.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Units.Commands.UpdateUnit
{
    public record UpdateUnitCommand : IRequest<UnitHasIdDTO>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int SyllabusSession { get; init; }
        public int UnitNumber { get; init; }
        public int SyllabusId { get; init; }
    }

    public class UpdateUnitHandler : IRequestHandler<UpdateUnitCommand, UnitHasIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly ICurrentTime _currentTime;
        public UpdateUnitHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
            _currentTime = currentTime;
        }

        public async Task<UnitHasIdDTO> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _unitOfWork.UnitRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (unit == null)
            {
                throw new NotFoundException("Unit not found");
            }
            unit = _mapper.Map<Domain.Entities.Unit>(request);
            unit.ModificationBy = _claimService.CurrentUserId;
            unit.ModificationDate = _currentTime.GetCurrentTime();
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.UnitRepository.Update(unit);
            });
            var result = _mapper.Map<UnitHasIdDTO>(unit);
            return result;
        }
    }
}
