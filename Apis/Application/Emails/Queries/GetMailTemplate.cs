using Domain;
using MediatR;
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
        private readonly AppConfiguration _config;

        public GetMailTemplateHandler(AppConfiguration config)
        {
            _config = config;
        }

        public async Task<string> Handle(GetMailTemplateQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine(Directory.GetCurrentDirectory() + "Here!!!!");
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(_config.HtmlTemplatePath))
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