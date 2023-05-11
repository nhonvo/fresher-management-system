using Application.Common.Exceptions;
using Application.Units.DTO;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Units.Commands.CreateUnit
{
    public record CreateUnitCommand : IRequest<UnitDTO>
    {
        public string Name { get; set; }
        public int SyllabusSession { get; set; }
        public int UnitNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }

    public class CreateUnitHandler : IRequestHandler<CreateUnitCommand, UnitDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateUnitHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitDTO> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = _mapper.Map<Domain.Entities.Unit>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.UnitRepository.AddAsync(unit);
            });
            var result = _mapper.Map<UnitDTO>(unit);

            return result ?? throw new NotFoundException("Unit not found");
        }
    }
}
