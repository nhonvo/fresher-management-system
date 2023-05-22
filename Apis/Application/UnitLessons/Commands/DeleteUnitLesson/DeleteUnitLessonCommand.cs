using Application.Common.Exceptions;
using Application.UnitLessons.DTO;
using AutoMapper;
using MediatR;

namespace Application.UnitLessons.Commands.DeleteUnitLesson
{
    public record DeleteUnitLessonCommand(int id) : IRequest<UnitLessonDTO>;
    public class DeleteUnitLessonHandler : IRequestHandler<DeleteUnitLessonCommand, UnitLessonDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteUnitLessonHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UnitLessonDTO> Handle(DeleteUnitLessonCommand request, CancellationToken cancellationToken)
        {
            var unitlesson = await _unitOfWork.LessonRepository.GetByIdAsync(request.id);
            if (unitlesson == null)
                throw new NotFoundException("Unit Lesson not found");
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.LessonRepository.Delete(unitlesson);
                });
                var result = _mapper.Map<UnitLessonDTO>(unitlesson);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
