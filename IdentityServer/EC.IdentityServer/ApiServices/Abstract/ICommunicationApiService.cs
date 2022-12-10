using Core.Entities;
using EC.IdentityServer.Dtos;

namespace EC.IdentityServer.ApiServices.Abstract
{
    public interface ICommunicationApiService
    {
        Task<IResult> SendSmtpEmailAsync(EmailData model);
    }
}
