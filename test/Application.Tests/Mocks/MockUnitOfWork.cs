using Moq;

namespace Application.Tests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockSyllabusRepo = MockSyllabusRepository.GetSyllabusRepository();

            mockUow.Setup(r => r.SyllabusRepository).Returns(mockSyllabusRepo.Object);

            return mockUow;
        }
    }
}
