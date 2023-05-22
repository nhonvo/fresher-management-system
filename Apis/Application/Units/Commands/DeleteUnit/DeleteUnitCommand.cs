using Application.Common.Exceptions;
using Application.Units.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Units.Commands.DeleteUnit
{
    public record DeleteUnitCommand(int id) : IRequest<UnitHasIdDTO>;
    public class DeleteUnitHandler : IRequestHandler<DeleteUnitCommand, UnitHasIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteUnitHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitHasIdDTO> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _unitOfWork.UnitRepository.FirstOrDefaultAsync(x => x.Id == request.id);
            if (unit == null)
            {
                throw new NotFoundException("Unit not found");
            }
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.UnitRepository.Delete(unit);
            });
            var result = _mapper.Map<UnitHasIdDTO>(unit);
            return result;
        }
    }
}
