// using AutoMapper;
// using Moq;
// using Shouldly;

// namespace Application.Tests.Syllabuses.Commands
// {
//     public class CreateSyllabusCommandHandlerTests
//     {
//         private readonly IMapper _mapper;
//         private readonly Mock<IUnitOfWork> _mockUow;
//         private readonly CreateSyllabusDto _syllabusDto;
//         private readonly CreateSyllabusCommandHandler _handler;

//         public CreateSyllabusCommandHandlerTests()
//         {
//             _mockUow = MockUnitOfWork.GetUnitOfWork();
            
//             var mapperConfig = new MapperConfiguration(c => 
//             {
//                 c.AddProfile<MappingProfile>();
//             });

//             _mapper = mapperConfig.CreateMapper();
//             _handler = new CreateSyllabusCommandHandler(_mockUow.Object, _mapper);

//             _syllabusDto = new CreateSyllabusDto
//             {
//                 DefaultDays = 15,
//                 Name = "Test DTO"
//             };
//         }

//         [Fact]
//         public async Task Valid_Syllabus_Added()
//         {
//             var result = await _handler.Handle(new CreateSyllabusCommand() { SyllabusDto = _syllabusDto }, CancellationToken.None);

//             var syllabuss = await _mockUow.Object.SyllabusRepository.GetAll();

//             result.ShouldBeOfType<BaseCommandResponse>();

//             syllabuss.Count.ShouldBe(4);
//         }

//         [Fact]
//         public async Task InValid_Syllabus_Added()
//         {
//             _syllabusDto.DefaultDays = -1;

//             var result = await _handler.Handle(new CreateSyllabusCommand() { SyllabusDto = _syllabusDto }, CancellationToken.None);

//             var syllabuss = await _mockUow.Object.SyllabusRepository.GetAll();
            
//             syllabuss.Count.ShouldBe(3);

//             result.ShouldBeOfType<BaseCommandResponse>();
            
//         }
//     }
// }
