using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Communication.RabbitMQClientServices
{
    public class PhotoWatermarkClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IElasticSearchLogService _logger;
        private IConnection _connection;
        private IModel _channel;
        public static readonly string ExchangeName = "ImageWatermarkExchange";
        public static readonly string RoutingWatermark = "route-image-watermark";
        public static readonly string QueueName = "queue-image-watermark";

        public PhotoWatermarkClientService(ConnectionFactory connectionFactory, IElasticSearchLogService logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true })
            {
                return _channel;
            }

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);
            _channel.QueueDeclare(QueueName, true, false, false, null);
            _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: RoutingWatermark);

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
