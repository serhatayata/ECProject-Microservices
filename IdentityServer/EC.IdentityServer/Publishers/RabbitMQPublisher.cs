using Core.CrossCuttingConcerns.Communication.RabbitMQClientServices;
using Core.Entities;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace EC.IdentityServer.Publishers
{
    public class RabbitMQPublisher
    {
        private readonly EmailSmtpClientService _emailSmtpClientService;

        public RabbitMQPublisher(EmailSmtpClientService emailSmtpClientService)
        {
            _emailSmtpClientService = emailSmtpClientService;
        }

        public void EmailSmtpSendPublish(EmailData model)
        {
            var channel = _emailSmtpClientService.Connect();

            var bodyString = JsonSerializer.Serialize(model);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: EmailSmtpClientService.ExchangeName, routingKey: EmailSmtpClientService.RoutingWatermark, basicProperties: properties, body: bodyByte);
        }

    }
}
