using Application.Commons;
using Application.Repositories;
using Domain.Entities;
using Moq;

namespace Application.Tests.Mocks
{
    public static class MockSyllabusRepository
    {
        public static Mock<ISyllabusRepository> GetSyllabusRepository()
        {
            var syllabuses = new List<Syllabus>
            {
                new Syllabus
                {
                    Id = 1,
                    Name = "Test Vacation"
                },
                new Syllabus
                {
                    Id = 2,
                    Name = "Test Sick"
                },
                new Syllabus
                {
                    Id = 3,
                    Name = "Test Maternity"
                }
            };


            var mockRepo = new Mock<ISyllabusRepository>();

            // mockRepo.Setup(r => r.GetAsync(
            //     pageIndex: 0,
            //     pageSize: 2)).ReturnsAsync(syllabuses);

            // mockRepo.Setup(r => r.AddAsync(It.IsAny<Syllabus>())).ReturnsAsync((Syllabus syllabus) =>
            // {
            //     syllabus.Add(syllabus);
            //     return syllabus;
            // });

            return mockRepo;

        }

    }
}
