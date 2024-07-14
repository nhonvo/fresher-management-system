using Application.Commons;
using Application.Lessons.DTO;
using AutoMapper;
using MediatR;

namespace Application.Lessons.Queries.GetUnitLessonByName
{
    public record GetUnitLessonByNameQuery(string? name, int pageIndex = 0, int pageSize = 10) : IRequest<Pagination<UnitLessonHasIdDTO>>;
    public class GetUnitLessonNameHandler : IRequestHandler<GetUnitLessonByNameQuery, Pagination<UnitLessonHasIdDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUnitLessonNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<UnitLessonHasIdDTO>> Handle(GetUnitLessonByNameQuery request, CancellationToken cancellationToken)
        {
            var unitlesson = await _unitOfWork.LessonRepository.GetAsync(
                filter: x => x.Name == request.name,
                pageIndex: request.pageIndex,
                pageSize: request.pageSize);

            var result = _mapper.Map<Pagination<UnitLessonHasIdDTO>>(unitlesson);

            return result;
        }
    }
}
