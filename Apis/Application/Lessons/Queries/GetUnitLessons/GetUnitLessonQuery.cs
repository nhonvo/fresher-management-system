using Application.Commons;
using Application.Lessons.DTO;
using AutoMapper;
using MediatR;

namespace Application.Lessons.Queries.GetUnitLessons
{
    public record GetUnitLessonQuery(int PageIndex = 0, int PageSize = 10) : IRequest<Pagination<UnitLessonHasIdDTO>>;

    public class GetUnitLessonHandler : IRequestHandler<GetUnitLessonQuery, Pagination<UnitLessonHasIdDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUnitLessonHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<UnitLessonHasIdDTO>> Handle(GetUnitLessonQuery request, CancellationToken cancellationToken)
        {
            var unitlesson = await _unitOfWork.LessonRepository.GetAsync(pageIndex: request.PageIndex, pageSize: request.PageSize);
            var result = _mapper.Map<Pagination<UnitLessonHasIdDTO>>(unitlesson);
            return result;
        }
    }
}
