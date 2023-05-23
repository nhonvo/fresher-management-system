using Application.Common.Exceptions;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace Application.Emails.Queries
{
    public record GetMailTemplateQuery : IRequest<string>
    {
        public string? speech { get; set; } = "";
        public string? title { get; set; } = "";
        public string? mainContent { get; set; } = "";
        public string? alternativeSpeech { get; set; } = "";
        public string? alternativeContent { get; set; } = "";
        public string? sign { get; set; } = "";
        public string? mainContentLink { get; set; } = "";
    };
    public class GetMailTemplateHandler : IRequestHandler<GetMailTemplateQuery, string>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly AppConfiguration _config;

        public GetMailTemplateHandler(
            IWebHostEnvironment hostingEnvironment,
            AppConfiguration config)
        {
            _hostingEnvironment = hostingEnvironment;
            _config = config;
        }

        public async Task<string> Handle(GetMailTemplateQuery request, CancellationToken cancellationToken)
        {
            // Console.WriteLine(Directory.GetCurrentDirectory() + "Here!!!!");
            string body = string.Empty;
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, _config.HtmlTemplatePath);
            if (!File.Exists(filePath))
            {
                throw new NotFoundException(nameof(GetMailTemplateQuery), "Html template not found");
            }
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line = "";
                StringBuilder stringBuilder = new StringBuilder();
                while ((line = reader.ReadLine()) != null)
                {
                    stringBuilder.Append(line);
                }
                body = stringBuilder.ToString();
            }
            body = body.Replace("{Title}", request.title);
            body = body.Replace("{Speech}", request.speech);
            body = body.Replace("{MainContentLink}", request.mainContentLink);
            body = body.Replace("{MainContent}", request.mainContent);
            body = body.Replace("{AlternativeSpeech}", request.alternativeSpeech);
            body = body.Replace("{AlternativeContent}", request.alternativeContent);
            body = body.Replace("{Sign}", request.sign);
            return body;
        }
    }
}