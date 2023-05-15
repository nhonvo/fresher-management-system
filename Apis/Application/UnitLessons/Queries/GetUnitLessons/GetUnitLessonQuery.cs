using Application.Commons;
using Application.UnitLessons.DTO;
using AutoMapper;
using Domain.Aggregate.AppResult;
using MediatR;

namespace Application.UnitLessons.Queries.GetUnitLessons
{
    public record GetUnitLessonQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<UnitLessonDTO>>;

    public class GetUnitLessonHandler : IRequestHandler<GetUnitLessonQuery, Pagination<UnitLessonDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUnitLessonHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<UnitLessonDTO>> Handle(GetUnitLessonQuery request, CancellationToken cancellationToken)
        {
            var unitlesson = await _unitOfWork.UnitLessonRepository.ToPagination(request.PageIndex, request.PageSize);
            var result = _mapper.Map<Pagination<UnitLessonDTO>>(unitlesson);
            return result;
        }
    }
}
