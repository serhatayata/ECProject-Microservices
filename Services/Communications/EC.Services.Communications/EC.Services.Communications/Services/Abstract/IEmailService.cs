using EC.Services.Communications.Models;
using Core.Entities;

namespace EC.Services.Communications.Services.Abstract
{
    public interface IEmailService
    {
        ValueTask<bool> SendSmtpEmail(EmailData emailData);
        ValueTask<bool> SendSmtpEmailWithAttachment(EmailDataWithAttachment emailData);
    }
}
