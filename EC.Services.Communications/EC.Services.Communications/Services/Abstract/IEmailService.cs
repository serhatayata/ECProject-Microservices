using EC.Services.Communications.Models;

namespace EC.Services.Communications.Services.Abstract
{
    public interface IEmailService
    {
        ValueTask<bool> SendSmtpEmail(EmailData emailData);
        ValueTask<bool> SendSmtpEmailWithAttachment(EmailDataWithAttachment emailData);
    }
}
