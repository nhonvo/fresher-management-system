using Application.Common.Exceptions;
using Application.Lessons.DTO;
using AutoMapper;
using MediatR;

namespace Application.Lessons.Queries.GetUnitLessonById
{
    public record GetUnitLessonByIdQuery(int id) : IRequest<UnitLessonHasIdDTO>;

    public class GetUnitLessonByIdHandler : IRequestHandler<GetUnitLessonByIdQuery, UnitLessonHasIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUnitLessonByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitLessonHasIdDTO> Handle(GetUnitLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var unitlesson = await _unitOfWork.LessonRepository.GetByIdAsync(request.id);
            var result = _mapper.Map<UnitLessonHasIdDTO>(unitlesson);
            return result ?? throw new NotFoundException("Unit Lesson not found", request.id);
        }
    }
}
