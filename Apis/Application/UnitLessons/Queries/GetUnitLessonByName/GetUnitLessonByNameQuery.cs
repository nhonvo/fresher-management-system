using Application.Commons;
using Application.UnitLessons.DTO;
using AutoMapper;
using MediatR;

namespace Application.UnitLessons.Queries.GetUnitLessonByName
{
    public record GetUnitLessonByNameQuery(string? name, int pageIndex = 0, int pageSize = 10) : IRequest<Pagination<UnitLessonDTO>>;
    public class GetUnitLessonNameHandler : IRequestHandler<GetUnitLessonByNameQuery, Pagination<UnitLessonDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUnitLessonNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<UnitLessonDTO>> Handle(GetUnitLessonByNameQuery request, CancellationToken cancellationToken)
        {
            var unitlesson = await _unitOfWork.UnitLessonRepository.GetAsync(
                filter: x => x.Name == request.name,
                pageIndex: request.pageIndex,
                pageSize: request.pageSize);

            var result = _mapper.Map<Pagination<UnitLessonDTO>>(unitlesson);

            return result;
        }
    }
}
