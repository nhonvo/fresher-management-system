using Application.Common.Exceptions;
using Application.UnitLessons.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.UnitLessons.Commands.UpdateUnitLesson
{
    public record UpdateUnitLessonCommand : IRequest<UnitLessonDTO>
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int Duration { get; set; }
        public LessonType LessonType { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public int SortOrder { get; set; }
    }

    public class UpdateUnitLessonHandler : IRequestHandler<UpdateUnitLessonCommand, UnitLessonDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateUnitLessonHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitLessonDTO> Handle(UpdateUnitLessonCommand request, CancellationToken cancellationToken)
        {
            var unitlesson = await _unitOfWork.UnitLessonRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (unitlesson == null)
                throw new NotFoundException("Unit Lesson not found");
            unitlesson = _mapper.Map<UnitLesson>(request);
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.UnitLessonRepository.Update(unitlesson);
                await _unitOfWork.CommitAsync();
                var result = _mapper.Map<UnitLessonDTO>(unitlesson);
                return result;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new NotFoundException("Update has some Error");
            }
        }
    }
}
