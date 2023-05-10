using Application.Commons;
using Application.Units.DTO;
using AutoMapper;
using Domain.Aggregate.AppResult;
using MediatR;

namespace Application.Units.Queries.GetUnits
{
    public record GetUnitQuery(int PageIndex = 0, int PageSize = 10) : IRequest<ApiResult<Pagination<UnitDTO>>>;

    public class GetUnitHandler : IRequestHandler<GetUnitQuery, ApiResult<Pagination<UnitDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUnitHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult<Pagination<UnitDTO>>> Handle(GetUnitQuery request, CancellationToken cancellationToken)
        {
            var unit = await _unitOfWork.UnitRepository.ToPagination(request.PageIndex, request.PageSize);
            var result = _mapper.Map<Pagination<UnitDTO>>(unit);
            return new ApiSuccessResult<Pagination<UnitDTO>>(result);
        }
    }
}
