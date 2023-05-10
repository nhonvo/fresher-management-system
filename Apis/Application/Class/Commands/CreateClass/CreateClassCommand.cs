using Application.Class.DTO;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Class.Commands.CreateClass
{
    public record CreateClassCommand : IRequest<ClassDTO>
    {
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public DateTime ClassTimeStart { get; set; }
        public DateTime ClassTimeEnd { get; set; }
        public DateTime ReviewOn { get; set; }
        public DateTime ApproveOn { get; set; }
        public int NumberAttendeePlanned { get; set; }
        public int NumberAttendeeAccepted { get; set; }
        public int NumberAttendeeActual { get; set; }
        public ClassLocation ClassLocation { get; set; }
        public ClassStatus Status { get; set; }
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
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ClassRepository.AddAsync(trainingClass);
            });
            var result = _mapper.Map<ClassDTO>(trainingClass);
            return result ?? throw new NotFoundException("Class not found");
        }
    }
}
