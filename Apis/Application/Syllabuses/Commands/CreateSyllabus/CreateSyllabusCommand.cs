using Application.Common.Exceptions;
using Application.Syllabuses.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Syllabuses.Commands.CreateSyllabus
{
    // TODO: not use api result customize the response.
    public record CreateSyllabusCommand : IRequest<SyllabusDTO>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int AttendeeNumber { get; set; }
        public string CourseObjective { get; set; }
        public SyllabusLevel SyllabusLevel { get; set; }
        public float QuizScheme { get; set; }
        public float AssignmentScheme { get; set; }
        public float FinalScheme { get; set; }
        public float FinalTheoryScheme { get; set; }
        public float FinalPracticeScheme { get; set; }
        public float GPAScheme { get; set; }
        public List<SyllabusUnit> Units { get; set; }
    }
    public record SyllabusUnit
    {
        public string Name { get; set; }
        public int SyllabusSession { get; set; }
        public int UnitNumber { get; set; }
        public List<LessonUnit> UnitLessons { get; set; }
    }
    public class LessonUnit
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public LessonType LessonType { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public List<LessonTrainingMaterial> TrainingMaterials { get; set; }
    }
    public class LessonTrainingMaterial
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }

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
                // await _unitOfWork.ExecuteTransactionAsync(() =>
                // {
                // });
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