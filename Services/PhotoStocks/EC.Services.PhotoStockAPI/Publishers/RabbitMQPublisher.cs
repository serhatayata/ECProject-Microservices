using Core.CrossCuttingConcerns.Communication.RabbitMQClientServices;
using Core.Entities;
using EC.Services.PhotoStockAPI.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace EC.Services.PhotoStockAPI.Publishers
{
    internal class RabbitMQPublisher
    {
        private readonly PhotoWatermarkClientService _photoWatermarkClientService;

        public RabbitMQPublisher(PhotoWatermarkClientService photoWatermarkClientService)
        {
            _photoWatermarkClientService = photoWatermarkClientService;
        }

        public void PhotoWatermarkPublish(PhotoWatermarkEvent model)
        {
            var channel = _photoWatermarkClientService.Connect();

            var bodyString = JsonSerializer.Serialize(model);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: PhotoWatermarkClientService.ExchangeName, routingKey: PhotoWatermarkClientService.RoutingWatermark, basicProperties: properties, body: bodyByte);
        }
    }
}
