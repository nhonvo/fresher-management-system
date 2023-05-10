using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.TestAssessmentViewModels;
using AutoMapper;

namespace Application.Services
{
    public class TestAssessmentService : ITestAssessmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestAssessmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TestAssessmentViewModel>> GetChemicalAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TestAssessmentViewModel>> GetTestAssessmentPagingsionAsync(int pageIndex = 0, int pageSize = 10)
        {
            var chemicals = await _unitOfWork.TestAssessmentRepository.ToPagination(pageIndex, pageSize);
            var result = _mapper.Map<List<TestAssessmentViewModel>>(chemicals);
            return result;
        }
    }
}
