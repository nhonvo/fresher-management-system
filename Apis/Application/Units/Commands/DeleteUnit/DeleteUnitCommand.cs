using Application.Common.Exceptions;
using Application.Units.DTO;
using AutoMapper;
using MediatR;

namespace Application.Units.Commands.DeleteUnit
{
    public record DeleteUnitCommand(int id) : IRequest<UnitDTO>;
    public class DeleteUnitHandler : IRequestHandler<DeleteUnitCommand, UnitDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteUnitHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitDTO> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _unitOfWork.UnitRepository.GetByIdAsync(request.id);
            if (unit == null)
                throw new NotFoundException("Unit not found");
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.UnitRepository.Delete(unit);
                });
                var result = _mapper.Map<UnitDTO>(unit);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
