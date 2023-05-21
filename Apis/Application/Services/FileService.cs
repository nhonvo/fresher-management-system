using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                try
                {
                    string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return fileName;
        }
    }
}
