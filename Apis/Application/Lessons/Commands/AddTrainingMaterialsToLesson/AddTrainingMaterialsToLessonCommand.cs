using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Lessons.Commands.AddTrainingMaterialsToLesson;

public record AddTrainingMaterialsToLessonCommand : IRequest<List<TrainingMaterialDto>>
{
#pragma warning disable
    public int? Id { get; set; }
    public List<IFormFile> TrainingMaterials { get; init; }
}

public record AddTrainingMaterialsToLessonCommandHandler : IRequestHandler<AddTrainingMaterialsToLessonCommand, List<TrainingMaterialDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public AddTrainingMaterialsToLessonCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<List<TrainingMaterialDto>> Handle(
        AddTrainingMaterialsToLessonCommand request,
        CancellationToken cancellationToken)
    {
        var lesson = await _unitOfWork.LessonRepository.GetByIdAsync(request.Id);
        if (request.TrainingMaterials != null)
        {
            lesson.TrainingMaterials = new List<TrainingMaterial>() { };
            foreach (var item in request.TrainingMaterials)
            {
                lesson.TrainingMaterials.Add(new TrainingMaterial()
                {
                    FileName = item.FileName,
                    FileSize = item.Length,
                    FilePath = await _fileService.UploadFile(item),
                });
            };
        }
        _unitOfWork.LessonRepository.Update(lesson);
        await _unitOfWork.SaveChangesAsync();
        var result = _mapper.Map<List<TrainingMaterialDto>>(lesson.TrainingMaterials);
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
