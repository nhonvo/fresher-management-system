using Application.Common.Exceptions;
using Application.UnitLessons.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.UnitLessons.Commands.CreateUnitLesson
{
    public class CreateUnitLessonCommand : IRequest<UnitLessonDTO>
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public LessonType LessonType { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public int SortOrder { get; set; }
    }
    public class CreateUnitLessonHandler : IRequestHandler<CreateUnitLessonCommand, UnitLessonDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateUnitLessonHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitLessonDTO> Handle(CreateUnitLessonCommand request, CancellationToken cancellationToken)
        {
            var unitlesson = _mapper.Map<UnitLesson>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.UnitLessonRepository.AddAsync(unitlesson);
            });
            var result = _mapper.Map<UnitLesson>(unitlesson);

            return result ?? throw new NotFoundException("Unit Lesson not found");
        }
    }
}
