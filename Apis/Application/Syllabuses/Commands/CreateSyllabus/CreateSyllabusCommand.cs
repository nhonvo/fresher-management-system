using Application.Common.Exceptions;
using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Syllabuses.Commands.CreateSyllabus
{
    public record CreateSyllabusCommand : IRequest<SyllabusDTO>
    {
        public string Name { get; init; }
        public string Code { get; init; }
        public int AttendeeNumber { get; init; }
        public string CourseObjective { get; init; }
        public SyllabusLevel SyllabusLevel { get; init; }
        public float QuizScheme { get; init; }
        public float AssignmentScheme { get; init; }
        public float FinalScheme { get; init; }
        public float FinalTheoryScheme { get; init; }
        public float FinalPracticeScheme { get; init; }
        public float GPAScheme { get; init; }
        public List<SyllabusUnit> Units { get; init; }
    }
    public record SyllabusUnit
    {
        public string Name { get; init; }
        public int SyllabusSession { get; init; }
        public int UnitNumber { get; init; }
        public List<LessonUnit> UnitLessons { get; init; }
    }
    public class LessonUnit
    {
        public string Name { get; init; }
        public int Duration { get; init; }
        public LessonType LessonType { get; init; }
        public DeliveryType DeliveryType { get; init; }
        public List<LessonTrainingMaterial> TrainingMaterials { get; init; }
    }
    public class LessonTrainingMaterial
    {
        public string FileName { get; init; }
        public string FilePath { get; init; }
        public long FileSize { get; init; }

    }
    public class CreateSyllabusHandler : IRequestHandler<CreateSyllabusCommand, SyllabusDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateSyllabusHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SyllabusDTO> Handle(CreateSyllabusCommand request, CancellationToken cancellationToken)
        {
            var syllabus = _mapper.Map<Syllabus>(request);
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.SyllabusRepository.AddAsync(syllabus);
                await _unitOfWork.CommitAsync();
                var result = _mapper.Map<SyllabusDTO>(syllabus);

                return result;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new NotFoundException("Can not add Syllabus" + ex.ToString());
            }
        }
    }
}