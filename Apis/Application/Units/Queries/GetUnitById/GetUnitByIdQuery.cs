using Application.Common.Exceptions;
using Application.Units.DTO;
using AutoMapper;
using MediatR;

namespace Application.Units.Queries.GetUnitById
{
    public record GetUnitByIdQuery(int id) : IRequest<UnitDTO>;

    public class GetUnitByIdHandler : IRequestHandler<GetUnitByIdQuery, UnitDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUnitByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitDTO> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
        {
            var unit = await _unitOfWork.UnitRepository.GetByIdAsync(request.id);
            var result = _mapper.Map<UnitDTO>(unit);
            return result ?? throw new NotFoundException("Unit not found", request.id);
        }
    }
}
