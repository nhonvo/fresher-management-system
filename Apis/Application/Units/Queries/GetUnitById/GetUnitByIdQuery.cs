using Application.Common.Exceptions;
using Application.Units.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Units.Queries.GetUnitById
{
    public record GetUnitByIdQuery(int id) : IRequest<UnitHasIdDTO>;

    public class GetUnitByIdHandler : IRequestHandler<GetUnitByIdQuery, UnitHasIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUnitByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitHasIdDTO> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
        {
            var unit = await _unitOfWork.UnitRepository.GetByIdAsync(request.id);
            var result = _mapper.Map<UnitHasIdDTO>(unit);
            return result ?? throw new NotFoundException("Unit not found", request.id);
        }
    }
}
