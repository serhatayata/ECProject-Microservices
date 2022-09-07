using EC.IdentityServer.Models.Email;
using EC.IdentityServer.Models.Settings;
using EC.IdentityServer.Services.Abstract;
using IdentityServer4.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace EC.IdentityServer.Services.Concrete
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSetting _smtpSetting;

        public EmailSender(SmtpSetting smtpSetting)
        {
            _smtpSetting = smtpSetting;
        }

        #region SendEmail
        public void SendSmtpEmail(SmtpMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
        #endregion
        #region CreateEmailMessage
        private MimeMessage CreateEmailMessage(SmtpMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email",_smtpSetting.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }
        #endregion
        #region Send
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_smtpSetting.SmtpClient, _smtpSetting.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_smtpSetting.From, _smtpSetting.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
        #endregion



    }
}
