using Application.Common.Exceptions;
using Application.Syllabuses.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Syllabuses.Commands.UpdateSyllabus
{
    public record UpdateSyllabusCommand : IRequest<SyllabusDTO>
    {
        public int Id { get; init; }
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
                throw new NotFoundException("Update has some error");
            }

        }
    }
}