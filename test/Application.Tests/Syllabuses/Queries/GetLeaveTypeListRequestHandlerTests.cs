// using AutoMapper;
// using Moq;
// using Shouldly;

// namespace Application.Tests.Syllabuses.Queries
// {
//     public class GetSyllabusListRequestHandlerTests
//     {
//         private readonly IMapper _mapper;
//         private readonly Mock<ISyllabusRepository> _mockRepo;
//         public GetSyllabusListRequestHandlerTests()
//         {
//             _mockRepo = MockSyllabusRepository.GetSyllabusRepository();

//             var mapperConfig = new MapperConfiguration(c => 
//             {
//                 c.AddProfile<MappingProfile>();
//             });

//             _mapper = mapperConfig.CreateMapper();
//         }

//         [Fact]
//         public async Task GetSyllabusListTest()
//         {
//             var handler = new GetSyllabusListRequestHandler(_mockRepo.Object, _mapper);

//             var result = await handler.Handle(new GetSyllabusListRequest(), CancellationToken.None);

//             result.ShouldBeOfType<List<SyllabusDto>>();

//             result.Count.ShouldBe(3);
//         }
//     }
// }
