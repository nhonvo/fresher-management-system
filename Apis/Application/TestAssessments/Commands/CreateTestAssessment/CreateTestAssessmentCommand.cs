using Application.Common.Exceptions;
using Application.TestAssessments.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.Enums.TestAssessmentEnums;
using MediatR;

namespace Application.TestAssessments.Commands.CreateTestAssessment
{
    public record CreateTestAssessmentCommand : IRequest<TestAssessmentDTO>
    {
        public float Score { get; set; }
        public TestAssessmentType TestAssessmentType { get; set; }
        public int AttendeeId { get; set; }
        public int SyllabusId { get; set; }
    }
    public class CreateTestAssessmentHandler : IRequestHandler<CreateTestAssessmentCommand, TestAssessmentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateTestAssessmentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TestAssessmentDTO> Handle(CreateTestAssessmentCommand request, CancellationToken cancellationToken)
        {
            var test = _mapper.Map<TestAssessment>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.TestAssessmentRepository.AddAsync(test);
            });
            var result = _mapper.Map<TestAssessmentDTO>(test);

            return result ?? throw new NotFoundException("Can not add test assessment");
        }
    }
}