using Application.Lectures.DTO;
using AutoMapper;
using Domain.Aggregate.AppResult;
using Domain.Enums.LectureEnums;
using MediatR;

namespace Application.Lectures.Commands
{
    public record CreateLectureCommand : IRequest<ApiResult<LectureDTO>>
    {
        public string Name { get; set; }
        public int? UnitId { get; set; }
        public int Duration { get; set; }
        public LectureLessonType LessonType { get; set; }
        public LectureDeliveryType DeliveryType { get; set; }
        public int? OutputStandardId { get; set; }
    }
    public class CreateLectureHandle : IRequestHandler<CreateLectureCommand, ApiResult<LectureDTO>>
    {
         private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateLectureHandle(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<ApiResult<LectureDTO>> Handle(CreateLectureCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}