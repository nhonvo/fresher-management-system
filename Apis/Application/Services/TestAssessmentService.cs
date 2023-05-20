using Application.Common.Exceptions;
using Application.Commons;
using Application.Interfaces;
using Application.Syllabuses.DTO;
using Application.TestAssessments.Queries.GetListSyllabusScoreOfStudent;
using Application.ViewModels.TestAssessmentViewModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;

namespace Application.Services
{
    public class TestAssessmentService : ITestAssessmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IFileService _fileService;

        public TestAssessmentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator,
            IFileService fileService
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
            _fileService = fileService;

        }

        public async Task<TestAssessmentViewModel?> CreateTestAssessmentAsync(CreateTestAssessmentViewModel request)
        {
            var obj = _mapper.Map<TestAssessment>(request);
            if (request.FileMaterials != null)
            {
                obj.TrainingMaterials = new List<TrainingMaterial>() { };
                foreach (var item in request.FileMaterials)
                {
                    obj.TrainingMaterials.Add(new TrainingMaterial()
                    {
                        FileName = item.FileName,
                        FileSize = item.Length,
                        FilePath = await _fileService.UploadFile(item),
                    });
                };
            }
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

        public async Task<Pagination<GetListSyllabusScoreOfStudentViewModel>> GetListSyllabusScoreOfStudentAsync(int id, int? classId, int pageIndex, int pageSize)
        {
            var query = new GetListSyllabusScoreOfStudentQuery
            {
                Id = id,
                ClassId = classId,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            result = _mapper.Map<Pagination<GetListSyllabusScoreOfStudentViewModel>>(result);
            return result ?? throw new NotFoundException("There is no test assessment for this student");
        }

        public async Task<Pagination<GetListSyllabusScoreOfClassViewModel>> GetListSyllabusScoreOfClassAsync(int id, int? studentId, int pageIndex = 0, int pageSize = 10)
        {
            Expression<Func<TestAssessment, bool>> filter = studentId == null ? x => x.TrainingClassId == id && x.Score != null :
                x => x.AttendeeId == studentId && x.TrainingClassId == id && x.Score != null;
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
                TotalItemsCount = count,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = classFinalSyllabusScore
            };
            result = _mapper.Map<Pagination<GetListSyllabusScoreOfClassViewModel>>(result);
            return result ?? throw new NotFoundException("There is no test assessment for this class");
        }


        public async Task<Pagination<GetClassGPAScoreOfStudentViewModel>> GetClassGPAScoreOfStudentAsync(int id, int? classId, int pageIndex = 0, int pageSize = 10)
        {
            var classFinalSyllabusScore = await GetListSyllabusScoreOfStudentAsync(id, classId, 0, 0);

            var studentGPAScoreOfClass = classFinalSyllabusScore.Items.GroupBy(x => new { x.TrainingClassId }).Select(group => new GetClassGPAScoreOfStudentViewModel
            {
                AttendeeId = id,
                ClassId = group.Key.TrainingClassId,
                GPA = classFinalSyllabusScore.Items.Where(x => x.TrainingClassId == group.Key.TrainingClassId).SelectMany(x => x.ListAssessment).Average(x => x.AverageScore),
                ListSyllabus = classFinalSyllabusScore.Items.Where(x => x.TrainingClassId == group.Key.TrainingClassId).ToList()
            }).ToList();

            var count = studentGPAScoreOfClass.Count();
            studentGPAScoreOfClass = studentGPAScoreOfClass
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();

            var result = new Pagination<GetClassGPAScoreOfStudentViewModel>()
            {
                TotalItemsCount = count,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = studentGPAScoreOfClass,

            };
            result = _mapper.Map<Pagination<GetClassGPAScoreOfStudentViewModel>>(result);

            return result ?? throw new NotFoundException("There is no test assessment for this class");

        }

        public async Task<Pagination<GetStudentGPAScoreOfClassViewModel>> GetStudentGPAScoreOfClassAsync(int id, int? studentId, int pageIndex = 0, int pageSize = 10)
        {
            var classFinalSyllabusScore = await GetListSyllabusScoreOfClassAsync(id, studentId, 0, 0);
            var classGPAScoreOfStudent = classFinalSyllabusScore.Items.GroupBy(x => new { x.AttendeeId }).Select(group => new GetStudentGPAScoreOfClassViewModel
            {
                ClassId = id,
                AttendeeId = group.Key.AttendeeId,
                GPA = classFinalSyllabusScore.Items.Where(x => x.AttendeeId == group.Key.AttendeeId).SelectMany(x => x.ListAssessment).Average(x => x.AverageScore),
                ListSyllabus = classFinalSyllabusScore.Items.Where(x => x.AttendeeId == group.Key.AttendeeId).ToList()
            }).ToList();

            var count = classGPAScoreOfStudent.Count();
            if (pageSize != 0)
            {
                classGPAScoreOfStudent = classGPAScoreOfStudent
                .Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();
            }

            var result = new Pagination<GetStudentGPAScoreOfClassViewModel>()
            {
                TotalItemsCount = count,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = classGPAScoreOfStudent
            };
            result = _mapper.Map<Pagination<GetStudentGPAScoreOfClassViewModel>>(result);

            return result ?? throw new NotFoundException("There is no test assessment for this class");
        }
    }
}
