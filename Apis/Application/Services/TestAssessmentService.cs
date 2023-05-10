using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.TestAssessmentViewModels;
using AutoMapper;
using Domain.Entities;

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

        public async Task<TestAssessmentViewModel?> CreateChemicalAsync(CreateTestAssessmentViewModel request)
        {
            var obj = _mapper.Map<TestAssessment>(request);
            await _unitOfWork.TestAssessmentRepository.AddAsync(obj);
            var isSuccess = await _unitOfWork.SaveChangesAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<TestAssessmentViewModel>(obj);
            }
            return null;
        }

        public async Task<List<TestAssessmentViewModel>> GetChemicalAsync()
        {
            var chemicals = await _unitOfWork.TestAssessmentRepository.GetAsync();
            var result = _mapper.Map<List<TestAssessmentViewModel>>(chemicals);
            return result;
        }

        public async Task<List<TestAssessmentViewModel>> GetTestAssessmentPagingsionAsync(int pageIndex = 0, int pageSize = 10)
        {
            var chemicals = await _unitOfWork.TestAssessmentRepository.ToPagination(pageIndex, pageSize);
            var result = _mapper.Map<List<TestAssessmentViewModel>>(chemicals);
            return result;
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TestAssessmentViewModel> UpdateAsync(int id, UpdateTestAssessmentViewModel updateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
