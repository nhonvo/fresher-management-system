using Application.Class.DTO;
using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Class.Commands.CreateClass
{
    public record CreateClassCommand : IRequest<ClassDTO>
    {
        public string ClassName { get; set; }
        public DateTime ClassTimeStart { get; set; }
        public DateTime ClassTimeEnd { get; set; }
        public DateTime ReviewOn { get; set; }
        public DateTime ApproveOn { get; set; }
        public AttendeeType AttendeeType { get; set; }
        public int NumberAttendeePlanned { get; set; }
        public int NumberAttendeeAccepted { get; set; }
        public int NumberAttendeeActual { get; set; }
        public ClassLocation ClassLocation { get; set; }
        public ClassStatus Status { get; set; }
    }
    public class CreateClassHandler : IRequestHandler<CreateClassCommand, ClassDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTService x;
        private readonly IMapper _mapper;
        public CreateClassHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ClassDTO> Handle(CreateClassCommand request, CancellationToken cancellationToken)
        {
            var trainingClass = _mapper.Map<TrainingClass>(request);
            trainingClass.ClassCode = await GenerateClassCode(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.ClassRepository.AddAsync(trainingClass);
            });
            var result = _mapper.Map<ClassDTO>(trainingClass);
            return result ?? throw new NotFoundException("Can not create class");
        }
        public async Task<string> GenerateClassCode(CreateClassCommand classDTO)
        {
            string hcmCode = classDTO.ClassLocation.ToString();
            string year = DateTime.Now.Year.ToString().Substring(2);
            string frCode = classDTO.AttendeeType switch
            {
                AttendeeType.Intern => "IN",
                AttendeeType.Fresher => "FR",
                AttendeeType.OnlineFeeFresher => "OF",
                AttendeeType.OfflineFeeFresher => "FF",
                _ => throw new ArgumentException($"Unknown AttendeeType: {classDTO.AttendeeType}")
            };
            string oCode = classDTO.ClassTimeStart.ToString("o").Substring(11, 1);
            int sequenceNumber = await _unitOfWork.ClassRepository.CountAsync();
            string classCode = $"{hcmCode}{year}_{frCode}.{oCode}_{classDTO.ClassName}_{sequenceNumber.ToString()}";
            return classCode;
        }
    }
}
