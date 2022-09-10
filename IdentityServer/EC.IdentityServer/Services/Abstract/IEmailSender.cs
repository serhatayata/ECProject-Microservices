using EC.IdentityServer.Models.Email;
using IdentityServer4.Models;

namespace EC.IdentityServer.Services.Abstract
{
    public interface IEmailSender
    {
        void SendSmtpEmail(SmtpMessage message);
    }
}
