using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;
using System.Text.Json;
using System.Transactions;

namespace Application.SeedData.Queries.SeedData
{

    public record SeedDataQuery : IRequest;
    public class SeedDataQueryHandler : IRequestHandler<SeedDataQuery>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeedDataQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(SeedDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString(), "Seeding error");
                throw new NotFoundException("Some seeding table failed " + ex.Message);
            }
        }
        public async Task TrySeedAsync()
        {
            if (!await _unitOfWork.UserRepository.AnyAsync())
            {
                string json = File.ReadAllText(@"../../Json/User.json");
                List<User> users = JsonSerializer.Deserialize<List<User>>(json)!;
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.UserRepository.AddRangeAsync(users);
                });
            };
            if (!await _unitOfWork.SyllabusRepository.AnyAsync())
            {
                string json = File.ReadAllText(@"../../Json/Syllabus.json");
                List<Syllabus> syllabuses = JsonSerializer.Deserialize<List<Syllabus>>(json)!;
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.SyllabusRepository.AddRangeAsync(syllabuses);
                });
            }

            if (!await _unitOfWork.TrainingProgramRepository.AnyAsync())
            {
                string json = File.ReadAllText(@"../../Json/TrainingProgram.json");
                List<TrainingProgram> trainingPrograms = JsonSerializer.Deserialize<List<TrainingProgram>>(json)!;
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.TrainingProgramRepository.AddRangeAsync(trainingPrograms);
                });
            }

            if (!await _unitOfWork.ClassRepository.AnyAsync())
            {
                string json = File.ReadAllText(@"../../Json/TrainingClass.json");
                List<TrainingClass> trainingClasses = JsonSerializer.Deserialize<List<TrainingClass>>(json)!;
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.ClassRepository.AddRangeAsync(trainingClasses);
                });
            }
           
            if (await _unitOfWork.CalenderRepository.AnyAsync() is false)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "../../Json/Calender.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    if (string.IsNullOrEmpty(json) is false)
                    {
                        var items = JsonSerializer.Deserialize<List<Calender>>(json);
                        if (items is not null)
                        {
                            await _unitOfWork.ExecuteTransactionAsync(() =>
                           {
                               _unitOfWork.CalenderRepository.AddRangeAsync(items);
                           });
                        }
                    }
                }
            }
            if (!await _unitOfWork.ClassStudentRepository.AnyAsync())
            {
                string json = File.ReadAllText(@"../../Json/ClassStudent.json");
                List<ClassStudent> classStudents = JsonSerializer.Deserialize<List<ClassStudent>>(json)!;
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.ClassStudentRepository.AddRangeAsync(classStudents);
                });
            }

            //if (!await _unitOfWork.TestAssessmentRepository.AnyAsync())
            //{
            //    string json = File.ReadAllText(@"../../Json/TestAssessment.json");
            //    List<TestAssessment> testAssessments = JsonSerializer.Deserialize<List<TestAssessment>>(json)!;
            //    await _unitOfWork.ExecuteTransactionAsync(() =>
            //    {
            //        _unitOfWork.TestAssessmentRepository.AddRangeAsync(testAssessments);
            //    });
            //}
            if (!await _unitOfWork.UnitRepository.AnyAsync())
            {
                string json = File.ReadAllText(@"../../Json/Unit.json");
                List<Domain.Entities.Unit> testAssessments = JsonSerializer.Deserialize<List<Domain.Entities.Unit>>(json)!;
                await _unitOfWork.ExecuteTransactionAsync(() =>
               {
                   _unitOfWork.UnitRepository.AddRangeAsync(testAssessments);
               });
            }
        }
    }
}