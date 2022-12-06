using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.Extensions;
using EC.Services.Communications.Constants;
using EC.Services.Communications.Models;
using EC.Services.Communications.Models.Settings;
using EC.Services.Communications.Services.Abstract;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Reflection;

namespace EC.Services.Communications.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private EmailSettings _emailSettings;
        private IElasticSearchLogService _elasticSearchLogService;
        public EmailService(IElasticSearchLogService elasticSearchLogService,IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
            _elasticSearchLogService = elasticSearchLogService;
        }

        public async ValueTask<bool> SendSmtpEmail(EmailData emailData)
        {
            var method = MethodBase.GetCurrentMethod();

            try
            {
                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress(_emailSettings.Name, _emailSettings.EmailId);
                emailMessage.From.Add(emailFrom);
                MailboxAddress emailTo = new MailboxAddress(emailData.EmailToName, emailData.EmailToId);
                emailMessage.To.Add(emailTo);
                emailMessage.Subject = emailData.EmailSubject;
                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = emailData.EmailBody;
                emailMessage.Body = emailBodyBuilder.ToMessageBody();
                SmtpClient emailClient = new SmtpClient();
                emailClient.Connect(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSSL);
                emailClient.Authenticate(_emailSettings.EmailId, _emailSettings.Password);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                var message = $"{MessageExtensions.NotCompleted(CommConstantValues.Email)} - To : {emailData.EmailToId} - Error : {ex.InnerException?.Message}";
                var logDetailError = LogExtensions.GetLogDetails(method, (int)LogDetailRisks.Normal, DateTime.Now.ToString(), message);

                await _elasticSearchLogService.AddAsync(logDetailError);
                return false;
            }
        }
    }
}
