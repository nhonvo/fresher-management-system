using Application.Common.Exceptions;
using Application.Lessons.DTO;
using AutoMapper;
using MediatR;

namespace Application.Lessons.Commands.DeleteUnitLesson
{
    public record DeleteUnitLessonCommand(int id) : IRequest<UnitLessonHasIdDTO>;
    public class DeleteUnitLessonHandler : IRequestHandler<DeleteUnitLessonCommand, UnitLessonHasIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteUnitLessonHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitLessonHasIdDTO> Handle(DeleteUnitLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _unitOfWork.LessonRepository.GetByIdAsync(request.id);
            if (lesson == null)
            {
                throw new NotFoundException("Unit Lesson not found");
            }
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.LessonRepository.Delete(lesson);
                });
                var result = _mapper.Map<UnitLessonHasIdDTO>(lesson);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
