using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeTypes;

namespace Application.TrainingMaterials.Queries.DownloadTrainingMateria;

public record DownloadTrainingMaterialQuery(int id) : IRequest<FileStreamResult>;

public class DownloadTrainingMaterialQueryHandler : IRequestHandler<DownloadTrainingMaterialQuery, FileStreamResult>
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IUnitOfWork _unitOfWork;

    public DownloadTrainingMaterialQueryHandler(
        IWebHostEnvironment hostingEnvironment,
        IUnitOfWork unitOfWork)
    {
        _hostingEnvironment = hostingEnvironment;
        _unitOfWork = unitOfWork;
    }

    public async Task<FileStreamResult> Handle(DownloadTrainingMaterialQuery request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.TrainingMaterialRepository.GetByIdAsyncAsNoTracking(id: request.id);

        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, $"uploads/{item.FilePath}");
        // check if file not exist
        if (!File.Exists(filePath))
        {
            throw new NotFoundException(nameof(TrainingMaterial), $"{request.id} file by filePath not found");
        }
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var contentType = MimeTypeMap.GetMimeType(filePath);
        var result = new FileStreamResult(fileStream, contentType)
        {
            FileDownloadName = item.FileName
        };
        return result;
    }
}
