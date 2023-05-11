using Application.Common.Exceptions;
using Application.UnitLessons.DTO;
using AutoMapper;
using MediatR;

namespace Application.UnitLessons.Queries.GetUnitLessonById
{
    public record GetUnitLessonByIdQuery(int id) : IRequest<UnitLessonDTO>;

    public class GetUnitLessonByIdHandler : IRequestHandler<GetUnitLessonByIdQuery, UnitLessonDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
         
        public GetUnitLessonByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitLessonDTO> Handle(GetUnitLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var unitlesson = await _unitOfWork.UnitLessonRepository.GetByIdAsync(request.id);
            var result = _mapper.Map<UnitLessonDTO>(unitlesson);
            return result ?? throw new NotFoundException("Unit Lesson not found", request.id);
        }
    }
}
