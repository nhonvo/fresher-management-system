using Application.Syllabuses.DTOs;
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

            await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.SyllabusRepository.AddAsync(syllabus);
                }
            );
            var result = _mapper.Map<SyllabusDTO>(syllabus);

            return result;
        }
    }
}