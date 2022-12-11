using Core.CrossCuttingConcerns.Communication.MessageQueue.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using EC.IdentityServer.ApiServices.Abstract;
using Microsoft.Extensions.Options;
using IResult = Core.Utilities.Results.IResult;

namespace EC.IdentityServer.ApiServices.Concrete
{
    public class CommunicationApiService:ICommunicationApiService
    {
        private readonly SourceOriginSettings _sourceSettings;
        private readonly IRabbitMQService _rabbitMqService;

        public CommunicationApiService(IRabbitMQService rabbitMQService,IOptions<SourceOriginSettings> sourceSettings)
        {
            _rabbitMqService = rabbitMQService;
            _sourceSettings = sourceSettings.Value;
        }

        #region SendSmtpEmailAsync
        public async Task<IResult> SendSmtpEmailAsync(EmailData model)
        {
            _rabbitMqService.SendEmailActivationSmtpEmail(model);
            return new SuccessResult();
        }
        #endregion

    }
}
