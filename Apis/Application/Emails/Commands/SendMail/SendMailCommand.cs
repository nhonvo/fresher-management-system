using Domain;
using MediatR;
using System.Net;
using System.Net.Mail;

namespace Application.Emails.Commands.SendMail
{
    public record SendMailCommand : IRequest<bool>
    {
        // Receiver
        public List<string> To { get; set; }
        public List<string>? Bcc { get; set; }
        public List<string>? Cc { get; set; }

        // Sender   
        public string? From { get; set; }

        public string? DisplayName { get; set; }

        public string? ReplyTo { get; set; }

        public string? ReplyToName { get; set; }

        // Content
        public string Subject { get; set; }

        public string? Body { get; set; }


    }
    public class SendMailCommandHandler : IRequestHandler<SendMailCommand, bool>
    {
        private readonly AppConfiguration config;
        public SendMailCommandHandler(AppConfiguration configuration)
        {
            config = configuration;
        }

        public async Task<bool> Handle(SendMailCommand mailData, CancellationToken cancellationToken)
        {
            var mail = new MailMessage();
            #region Sender & Receiver
            //sender
            mail.From = new MailAddress(mailData.From ?? config.MailConfigurations.From, config.MailConfigurations.DisplayName);
            mail.Sender = new MailAddress(mailData.From ?? config.MailConfigurations.From, mailData.DisplayName ?? config.MailConfigurations.DisplayName);

            //receiver
            foreach (var mailAddress in mailData.To)
            {
                mail.To.Add(new MailAddress(mailAddress));
            }
            //set Reply to if specified in request
            if (!string.IsNullOrEmpty(mailData.ReplyTo))
            {
                mail.ReplyToList.Add(new MailAddress(mailData.ReplyTo, mailData.ReplyToName));
            }

            // BCC
            // Check if a BCC was supplied in the request
            if (mailData.Bcc != null)
            {
                // Get only addresses where value is not null or with whitespace. x = value of address
                foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    mail.Bcc.Add(new MailAddress(mailAddress.Trim()));
            }

            // CC
            // Check if a CC address was supplied in the request
            if (mailData.Cc != null)
            {
                foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    mail.CC.Add(new MailAddress(mailAddress.Trim()));
            }
            #endregion

            #region Content

            //add content to mail message
            mail.Subject = mailData.Subject;
            mail.Body = mailData.Body;
            mail.IsBodyHtml = true;

            #endregion

            #region Send Mail
            var smtp = new SmtpClient(config.MailConfigurations.Host, config.MailConfigurations.Port);
            smtp.Credentials = new NetworkCredential(config.MailConfigurations.UserName, config.MailConfigurations.Password);
            smtp.EnableSsl = config.MailConfigurations.UseSSL;
            await smtp.SendMailAsync(mail);
            #endregion

            return true;
        }

        //TODO: schedule setup hangfire or quartz auto sendmail in mid night. 
        // 
    }
}