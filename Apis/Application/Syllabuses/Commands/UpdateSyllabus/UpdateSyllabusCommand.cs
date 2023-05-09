using Application.Common.Exceptions;
using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Aggregate.AppResult;
using Domain.Entities.Syllabuses;
using Domain.Enums.SyllabusEnums;
using MediatR;

namespace Application.Syllabuses.Commands.UpdateSyllabus
{
    // TODO: not use api result customize the response.
    public record UpdateSyllabusCommand : IRequest<SyllabusDTO>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public float Version { get; set; }
        public string Name { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public SyllabusLevel Level { get; set; }
        public int AttendeeNumber { get; set; }
        public string CourseObjectives { get; set; }
        public string TechnicalRequirements { get; set; }
        public string TrainingDeliveryPrinciple { get; set; }
        public float QuizCriteria { get; set; }
        public float AssignmentCriteria { get; set; }
        public float FinalCriteria { get; set; }
        public float FinalTheoryCriteria { get; set; }
        public float FinalPracticalCriteria { get; set; }
        public float PassingGPA { get; set; }
        public bool isActive { get; set; }
        public int Duration { get; set; }
    }
    public class UpdateSyllabusHandler : IRequestHandler<UpdateSyllabusCommand, SyllabusDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateSyllabusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SyllabusDTO> Handle(UpdateSyllabusCommand request, CancellationToken cancellationToken)
        {
            var syllabus = await _unitOfWork.SyllabusRepository.GetByIdAsync(request.Id);

            var syllabusa = _mapper.Map<Syllabus>(request);
            await _unitOfWork.ExecuteTransactionAsync(() => { _unitOfWork.SyllabusRepository.AddAsync(syllabusa); });
            var result = _mapper.Map<SyllabusDTO>(syllabusa);

            return result ?? throw new NotFoundException("Syllabus not found");
        }
    }
}