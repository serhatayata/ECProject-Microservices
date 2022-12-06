using EC.IdentityServer.Models.Settings;
using EC.IdentityServer.Services.Abstract;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EC.IdentityServer.Services.Concrete
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSetting _smtpSetting;

        public EmailSender(IOptions<SmtpSetting> smtpSetting)
        {
            _smtpSetting = smtpSetting.Value;
        }

        #region SendEmail
        //public void SendSmtpEmail(SmtpMessage message)
        //{
        //    MailMessage msg = new MailMessage(); // Message Body Prepared;
        //    msg.Subject = message.Subject;
        //    msg.From = new MailAddress(_smtpSetting.From, _smtpSetting.From, System.Text.Encoding.UTF8);
        //    msg.To.Add(new MailAddress(message.To.First(),message.To.First()));
        //    msg.IsBodyHtml = true;
        //    msg.Body = $" <br> <br> Activation Code :  <p> {message.Content} </p>";

        //    //SMTP
        //    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        //    client.EnableSsl = true;
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = new NetworkCredential(_smtpSetting.From, _smtpSetting.Password);
        //    client.Send(msg);
        //}
        #endregion



    }
}
