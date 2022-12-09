using Core.Entities;
using EC.IdentityServer.ApiServices.Abstract;
using Microsoft.Extensions.Options;

namespace EC.IdentityServer.ApiServices.Concrete
{
    public class CommunicationApiService:ICommunicationApiService
    {
        private readonly SourceOriginSettings _sourceSettings;

        public CommunicationApiService(IOptions<SourceOriginSettings> sourceSettings)
        {
            _sourceSettings = sourceSettings.Value;
        }

        #region SendSmtpEmailAsync
        public async Task<IResult> SendSmtpEmailAsync(EmailData model)
        {
            //Send with rabbitmq
            throw new NotImplementedException();
        }
        #endregion

    }
}
