using Application.Commons;
using Application.Units.DTO;
using AutoMapper;
using MediatR;

namespace Application.Units.Queries.GetUnits
{
    public record GetUnitQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<UnitDTO>>;

    public class GetUnitHandler : IRequestHandler<GetUnitQuery, Pagination<UnitDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUnitHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<UnitDTO>> Handle(GetUnitQuery request, CancellationToken cancellationToken)
        {
            var unit = await _unitOfWork.UnitRepository.GetAsync(pageIndex: request.PageIndex, pageSize: request.PageSize);
            var result = _mapper.Map<Pagination<UnitDTO>>(unit);
            return result;
        }
    }
}
