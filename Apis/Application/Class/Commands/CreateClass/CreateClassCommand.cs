using Domain.Entities.Users;
using Domain.Entities;
using Domain.Enums.ClassEnums;
using MediatR;
using Application.Class.DTO;
using AutoMapper;
using Application.Common.Exceptions;

namespace Application.Class.Commands.CreateClass
{
    public record CreateClassCommand : IRequest<ClassDTO>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ClassLocation? Location { get; set; }
        public ClassAttendeeType? AttendeeType { get; set; }
        public ClassFSU? FSU { get; set; }
        public ClassTime ClassTime { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime FinishedOn { get; set; }
        public string LectureStartedTime { get; set; }
        public string LectureFinishedTime { get; set; }
        public int DaysDuration { get; set; }
        public int TimeDuration { get; set; }
        public ClassStatus Status { get; set; }
        public DateTime ApprovedOn { get; set; }
        public int? TrainingProgramId { get; set; }
    }
    public class CreateClassHandler : IRequestHandler<CreateClassCommand, ClassDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateClassHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ClassDTO> Handle(CreateClassCommand request, CancellationToken cancellationToken)
        {
            var trainingClass = _mapper.Map<TrainingClass>(request);
            await _unitOfWork.ExecuteTransactionAsync(() => { _unitOfWork.ClassRepository.AddAsync(trainingClass); });
            var result = _mapper.Map<ClassDTO>(trainingClass);
            return result ?? throw new NotFoundException("Class not found");
        }
    }
}
