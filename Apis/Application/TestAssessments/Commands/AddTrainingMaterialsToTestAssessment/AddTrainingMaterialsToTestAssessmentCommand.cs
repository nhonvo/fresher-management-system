using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.TestAssessments.Commands.AddTrainingMaterialsToTestAssessment;

public record AddTrainingMaterialsToTestAssessmentCommand : IRequest<List<TrainingMaterialDto>>
{
#pragma warning disable
    public int? Id { get; set; }
    public List<IFormFile> TrainingMaterials { get; init; }
}

public record AddTrainingMaterialsToTestAssessmentCommandHandler : IRequestHandler<AddTrainingMaterialsToTestAssessmentCommand, List<TrainingMaterialDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public AddTrainingMaterialsToTestAssessmentCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<List<TrainingMaterialDto>> Handle(AddTrainingMaterialsToTestAssessmentCommand request, CancellationToken cancellationToken)
    {
        var testAssessment = await _unitOfWork.TestAssessmentRepository.GetByIdAsync(request.Id);
        testAssessment.TrainingMaterials = new List<TrainingMaterial>() { };
        foreach (var item in request.TrainingMaterials)
        {
            testAssessment.TrainingMaterials.Add(new TrainingMaterial()
            {
                FileName = item.FileName,
                FileSize = item.Length,
                FilePath = await _fileService.UploadFile(item),
            });
        };
        _unitOfWork.TestAssessmentRepository.Update(testAssessment);
        await _unitOfWork.SaveChangesAsync();
        var result = _mapper.Map<List<TrainingMaterialDto>>(testAssessment.TrainingMaterials);
        return result;
    }
}

public class TrainingMaterialDto
{
    public int Id { get; init; }
    public int UnitLessonId { get; init; }
    public int TestAssessmentId { get; init; }
    public string FileName { get; init; }
    public string FilePath { get; init; }
    public long FileSize { get; init; }
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TrainingMaterial, TrainingMaterialDto>();
    }
}
