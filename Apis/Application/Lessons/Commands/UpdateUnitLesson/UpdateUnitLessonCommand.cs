using Application.Common.Exceptions;
using Application.Lessons.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Lessons.Commands.UpdateUnitLesson
{
    public record UpdateUnitLessonCommand : IRequest<UnitLessonHasIdDTO>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Duration { get; init; }
        public LessonType LessonType { get; init; }
        public DeliveryType DeliveryType { get; init; }
        public int UnitId { get; init; }
    }

    public class UpdateUnitLessonHandler : IRequestHandler<UpdateUnitLessonCommand, UnitLessonHasIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateUnitLessonHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitLessonHasIdDTO> Handle(UpdateUnitLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _unitOfWork.LessonRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (lesson == null)
            {
                throw new NotFoundException("Lesson not found");
            }
            lesson = _mapper.Map<Lesson>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.LessonRepository.Update(lesson);
            });

            var result = _mapper.Map<UnitLessonHasIdDTO>(lesson);
            return result;
        }
    }
}
