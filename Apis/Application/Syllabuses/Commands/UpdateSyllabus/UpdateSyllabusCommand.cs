using Application.Common.Exceptions;
using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Syllabuses.Commands.UpdateSyllabus
{
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
            var syllabus = await _unitOfWork.SyllabusRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (syllabus == null)
                throw new NotFoundException("Syllabus not found");
            syllabus = _mapper.Map<Syllabus>(request);
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.SyllabusRepository.Update(syllabus);
                await _unitOfWork.CommitAsync();
                var result = _mapper.Map<SyllabusDTO>(syllabus);
                return result;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new NotFoundException("Update has some error"); ;
            }

        }
    }
}