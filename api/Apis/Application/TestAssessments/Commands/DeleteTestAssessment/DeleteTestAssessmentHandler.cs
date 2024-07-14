using Application.Common.Exceptions;
using Application.TestAssessments.DTO;
using AutoMapper;
using MediatR;

namespace Application.TestAssessments.Commands.DeleteTestAssessment
{
    public record DeleteTestAssessmentCommand(int id) : IRequest<TestAssessmentDTO> { };
    public class DeleteTestAssessmentHandler : IRequestHandler<DeleteTestAssessmentCommand, TestAssessmentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteTestAssessmentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TestAssessmentDTO> Handle(DeleteTestAssessmentCommand request, CancellationToken cancellationToken)
        {
            var testAssessments = await _unitOfWork.TestAssessmentRepository.GetByIdAsync(request.id);
            if (testAssessments == null)
                throw new NotFoundException("TestAssessment not found");

            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.TestAssessmentRepository.Delete(testAssessments);
            });
            var result = _mapper.Map<TestAssessmentDTO>(testAssessments);
            return result ?? throw new NotFoundException("Can not delete test Assessment");
        }
    }
}
