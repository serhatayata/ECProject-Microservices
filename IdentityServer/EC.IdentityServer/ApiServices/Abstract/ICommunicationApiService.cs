using Core.Entities;
using IResult = Core.Utilities.Results.IResult;

namespace EC.IdentityServer.ApiServices.Abstract
{
    public interface ICommunicationApiService
    {
        Task<IResult> SendSmtpEmailAsync(EmailData model);
    }
}
