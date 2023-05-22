using Application.Common.Exceptions;
using Application.Lessons.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Lessons.Commands.CreateUnitLesson
{
    public record CreateUnitLessonCommand : IRequest<UnitLessonHasIdDTO>
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public LessonType LessonType { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public int UnitId { get; set; }
    }
    public class CreateUnitLessonHandler : IRequestHandler<CreateUnitLessonCommand, UnitLessonHasIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateUnitLessonHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitLessonHasIdDTO> Handle(CreateUnitLessonCommand request, CancellationToken cancellationToken)
        {

            var unit = _mapper.Map<Lesson>(request);

            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.LessonRepository.AddAsync(unit);
            });
            var result = _mapper.Map<UnitLessonHasIdDTO>(unit);

            return result;
        }
    }
}
