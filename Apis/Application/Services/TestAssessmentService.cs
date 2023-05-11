using Application.Common.Exceptions;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.TestAssessmentViewModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

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

        public async Task<TestAssessmentViewModel?> CreateTestAssessmentAsync(CreateTestAssessmentViewModel request)
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

        public async Task<List<TestAssessmentViewModel>> GetTestAssessmentAsync()
        {
            var testAssessments = await _unitOfWork.TestAssessmentRepository.GetAsync();
            var result = _mapper.Map<List<TestAssessmentViewModel>>(testAssessments);
            return result;
        }

        public async Task<Pagination<TestAssessmentViewModel>> GetTestAssessmentPagingsionAsync(int pageIndex = 0, int pageSize = 10)
        {
            var testAssessments = await _unitOfWork.TestAssessmentRepository.ToPagination(pageIndex, pageSize);
            var result = _mapper.Map<Pagination<TestAssessmentViewModel>>(testAssessments);
            return result;
        }

        public async Task RemoveAsync(int id)
        {
            var product = await _unitOfWork.TestAssessmentRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            await _unitOfWork.TestAssessmentRepository.Delete(id);
        }

        public async Task<TestAssessmentViewModel> UpdateAsync(int id, UpdateTestAssessmentViewModel updateDTO)
        {
            TestAssessment model = _mapper.Map<TestAssessment>(updateDTO);
            model.Id = id;
            await _unitOfWork.ExecuteTransactionAsync(() =>
            {
                _unitOfWork.TestAssessmentRepository.Update(model);
            });
            var productDto = _mapper.Map<TestAssessmentViewModel>(model);
            return productDto ?? throw new NotFoundException("Can not update test assessment");
        }

        public async Task<Pagination<GetListSyllabusSocreOfStudentViewModel>> GetListSyllabusScoreOfStudentAsync(int id, int? classId, int pageIndex, int pageSize)
        {
            Expression<Func<TestAssessment, bool>> filter = classId == null ? x => x.AttendeeId == id :
                x => x.AttendeeId == id && x.TrainingCLassId == classId;
            var scoreByTestType = await _unitOfWork.TestAssessmentRepository.GetFinalScoreAsync(filter);
            var studentFinalSyllabusScore = scoreByTestType.GroupBy(ta => new { ta.SyllabusId, ta.TrainingClassId }).Select(group => new GetListSyllabusSocreOfStudentViewModel
            {
                SyllabusId = group.Key.SyllabusId,
                TrainingClassId = group.Key.TrainingClassId,
                FinalSyllabusScore = group.Sum(ta => ta.AverageScore * ta.SyllabusScheme) / group.Sum(ta => ta.SyllabusScheme) ?? 0,
                ListAssessment = scoreByTestType.Where(x => x.SyllabusId == group.Key.SyllabusId && x.TrainingClassId == group.Key.TrainingClassId).ToList()
            }).ToList();
            var count = studentFinalSyllabusScore.Count();

            if (pageSize != 0)
            {
                studentFinalSyllabusScore = studentFinalSyllabusScore
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize).ToList();
            }

            var result = new Pagination<GetListSyllabusSocreOfStudentViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = studentFinalSyllabusScore
            };
            return result ?? throw new NotFoundException("There is no test assessment for this student");
        }

        public async Task<Pagination<GetListSyllabusScoreOfClassViewModel>> GetListSyllabusScoreOfClassAsync(int id, int? studentId, int pageIndex = 0, int pageSize = 10)
        {
            Expression<Func<TestAssessment, bool>> filter = studentId == null ? x => x.TrainingCLassId == id :
                x => x.AttendeeId == studentId && x.TrainingCLassId == id;
            var scoreByTestType = await _unitOfWork.TestAssessmentRepository.GetFinalScoreAsync(filter);
            var classFinalSyllabusScore = scoreByTestType.GroupBy(ta => new { ta.AttendeeId, ta.SyllabusId }).Select(group => new GetListSyllabusScoreOfClassViewModel
            {
                AttendeeId = group.Key.AttendeeId,
                SyllabusId = group.Key.SyllabusId,
                FinalSyllabusScore = group.Sum(ta => ta.AverageScore * ta.SyllabusScheme) / group.Sum(ta => ta.SyllabusScheme) ?? 0,
                ListAssessment = scoreByTestType.Where(x => x.SyllabusId == group.Key.SyllabusId && x.AttendeeId == group.Key.AttendeeId).ToList()
            }).OrderBy(x => x.AttendeeId).ToList();
            var count = classFinalSyllabusScore.Count();
            if (pageSize != 0)
            {
                classFinalSyllabusScore = classFinalSyllabusScore
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();
            }

            var result = new Pagination<GetListSyllabusScoreOfClassViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = classFinalSyllabusScore
            };
            return result ?? throw new NotFoundException("There is no test assessment for this class");
        }


        public async Task<Pagination<GetClassGPAScoreOfStudentViewModel>> GetClassGPAScoreOfStudentAsync(int id, int classId, int pageIndex = 0, int pageSize = 10)
        {
            var classFinalSyllabusScore = await GetListSyllabusScoreOfClassAsync(classId, id, 0, 0);

            var classGPAScoreOfStudent = classFinalSyllabusScore.Items.GroupBy(x => new { x.AttendeeId }).Select(group => new GetClassGPAScoreOfStudentViewModel
            {
                AttendeeId = group.Key.AttendeeId,
                GPA = group.Average(x => x.FinalSyllabusScore),
                ListSyllabus = classFinalSyllabusScore.Items.Where(x => x.AttendeeId == group.Key.AttendeeId).ToList()
            }).ToList();

            var count = classGPAScoreOfStudent.Count();
            if (pageSize != 0)
            {
                classGPAScoreOfStudent = classGPAScoreOfStudent
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();
            }

            var result = new Pagination<GetClassGPAScoreOfStudentViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = classGPAScoreOfStudent
            };
            return result ?? throw new NotFoundException("There is no test assessment for this class");
        }

        public async Task<Pagination<GetStudentGPAScoreOfClassViewModel>> GetStudentGPAScoreOfClassAsync(int id, int studentId, int pageIndex = 0, int pageSize = 10)
        {
            var classFinalSyllabusScore = await GetListSyllabusScoreOfStudentAsync(studentId, id, 0, 0);

            var studentGPAScoreOfClass = classFinalSyllabusScore.Items.GroupBy(x => new { x.TrainingClassId }).Select(group => new GetStudentGPAScoreOfClassViewModel
            {
                ClassId = group.Key.TrainingClassId,
                GPA = group.Average(x => x.FinalSyllabusScore),
                ListSyllabus = classFinalSyllabusScore.Items.Where(x => x.TrainingClassId == group.Key.TrainingClassId).ToList()
            }).ToList();

            var count = studentGPAScoreOfClass.Count();
            studentGPAScoreOfClass = studentGPAScoreOfClass
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();

            var result = new Pagination<GetStudentGPAScoreOfClassViewModel>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = studentGPAScoreOfClass
            };
            return result ?? throw new NotFoundException("There is no test assessment for this class");
        }
    }
}
