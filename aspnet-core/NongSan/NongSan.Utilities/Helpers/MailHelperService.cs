using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using NongSan.Utilities.Models;
using NongSan.Utilities.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.Utilities.Helpers
{
    public class MailHelperService : IMailHelperService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MailSettings mailSettings;

        public MailHelperService(IOptions<MailSettings> _mailSettings,
            IWebHostEnvironment webHostEnvironment)
        {
            mailSettings = _mailSettings.Value;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task SendMail(MailContent mailContent)
        {
            var body = await GetBody(mailContent.FileName, mailContent.BodyKeyValue); 
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            email.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);
            }

            smtp.Disconnect(true);
        }

        private async Task<string> GetBody(string path, Dictionary<string, string> bodyKeyValue)
        {
            var fullPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Templates", path);
            var body = "";

            if (File.Exists(fullPath))
            {
                body = await File.ReadAllTextAsync(fullPath);

                if (bodyKeyValue != null)
                {
                    foreach (var item in bodyKeyValue)
                    {
                        body = body.Replace("{{" + item.Key + "}}", item.Value);
                    }
                }
            }

            return body;
        }
    }
}
