using Application.Common.Exceptions;
using Application.Units.DTO;
using AutoMapper;
using MediatR;

namespace Application.Units.Commands.UpdateUnit
{
    public record UpdateUnitCommand : IRequest<UnitDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SyllabusSession { get; set; }
        public int UnitNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int SyllabusId { get; set; }
    }

    public class UpdateUnitHandler : IRequestHandler<UpdateUnitCommand, UnitDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateUnitHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitDTO> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _unitOfWork.UnitRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (unit == null)
                throw new NotFoundException("Unit not found");
            unit = _mapper.Map<Domain.Entities.Unit>(request);
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.UnitRepository.Update(unit);
                await _unitOfWork.CommitAsync();
                var result = _mapper.Map<UnitDTO>(unit);
                return result;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new NotFoundException("Update has some Error");
            }
        }
    }
}
