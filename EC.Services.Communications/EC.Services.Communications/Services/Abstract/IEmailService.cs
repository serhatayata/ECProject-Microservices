using EC.Services.Communications.Models;

namespace EC.Services.Communications.Services.Abstract
{
    public interface IEmailService
    {
        ValueTask<bool> SendSmtpEmail(EmailData message);
    }
}
