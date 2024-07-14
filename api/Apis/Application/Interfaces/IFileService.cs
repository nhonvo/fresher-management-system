using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile file);

    }
}
