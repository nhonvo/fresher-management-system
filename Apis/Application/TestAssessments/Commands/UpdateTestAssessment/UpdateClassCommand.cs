using MediatR;
using AutoMapper;
using Application.Common.Exceptions;
using Application.TestAssessments.DTO;
using Domain.Entities;
using Domain.Enums.TestAssessmentEnums;

namespace Application.TestAssessments.Commands.UpdateTestAssessment
{
    public record UpdateTestAssessmentCommand : IRequest<TestAssessmentDTO>
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public TestAssessmentType TestAssessmentType { get; set; }
        public int AttendeeId { get; set; }
        public int SyllabusId { get; set; }
    }
    public record UpdateTestAssessmentHandler : IRequestHandler<UpdateTestAssessmentCommand, TestAssessmentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateTestAssessmentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TestAssessmentDTO> Handle(UpdateTestAssessmentCommand request, CancellationToken cancellationToken)
        {
            var test = await _unitOfWork.TestAssessmentRepository.GetByIdAsyncAsNoTracking(request.Id);
            if (test == null)
                throw new NotFoundException("TestAssessment not found");
            test = _mapper.Map<TestAssessment>(request);
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.TestAssessmentRepository.Update(test);
            });
            var result = _mapper.Map<TestAssessmentDTO>(test);
            return result ?? throw new NotFoundException("Can not update test Assessment");
        }
    }
}
