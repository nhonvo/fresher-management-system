using Application.Commons;
using Application.Units.DTO;
using AutoMapper;
using MediatR;

namespace Application.Units.Queries.GetUnitByName
{
    public record GetUnitByNameQuery(string? name, int pageIndex = 0, int pageSize = 10) : IRequest<Pagination<UnitDTO>>;
    public class GetUnitNameHandler : IRequestHandler<GetUnitByNameQuery, Pagination<UnitDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUnitNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<UnitDTO>> Handle(GetUnitByNameQuery request, CancellationToken cancellationToken)
        {
            var unit = await _unitOfWork.UnitRepository.GetAsync(
                filter: x => x.Name == request.name,
                pageIndex: request.pageIndex,
                pageSize: request.pageSize);

            var result = _mapper.Map<Pagination<UnitDTO>>(unit);

            return result;
        }
    }
}
