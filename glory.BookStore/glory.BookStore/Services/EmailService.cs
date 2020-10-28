using glory.BookStore.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace glory.BookStore.Services
{
    public class EmailService : IEmailService
    {
        //#104
        private const string templatePath = "EmailTemplate/TestEmail.html";

        private readonly SMTPConfigModel _sMTPConfig;

        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = "this is test email subject from glory";
            userEmailOptions.Body = GetEmailBody("Test Email");
            await SendEmail(userEmailOptions);
        }
        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _sMTPConfig = smtpConfig.Value;
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_sMTPConfig.SenderAddress, _sMTPConfig.SenderDisplayName),
                IsBodyHtml = _sMTPConfig.IsBodyHTML,
            };
            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }
            NetworkCredential networkCredential = new NetworkCredential(_sMTPConfig.UserName, _sMTPConfig.Password);
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _sMTPConfig.Host,
                Port = _sMTPConfig.Port,
                EnableSsl = _sMTPConfig.EnableSSL,
                UseDefaultCredentials = _sMTPConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);
        }
        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }
    }
}
