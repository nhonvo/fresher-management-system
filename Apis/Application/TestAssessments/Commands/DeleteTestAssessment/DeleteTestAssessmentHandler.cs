using MediatR;
using AutoMapper;
using Application.Common.Exceptions;
using Application.TestAssessments.DTO;

namespace Application.TestAssessments.Commands.DeleteTestAssessment
{
    public record DeleteTestAssessmentCommand(int Id) : IRequest<TestAssessmentDTO>{};
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
            var testAssessments = _unitOfWork.TestAssessmentRepository.GetByIdAsync(request.Id);
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
