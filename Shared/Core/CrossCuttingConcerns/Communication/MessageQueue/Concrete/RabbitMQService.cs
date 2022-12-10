using Core.CrossCuttingConcerns.Communication.MessageQueue.Abstract;
using Core.Entities;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Communication.MessageQueue.Concrete
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQService(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
            _connectionFactory = new ConnectionFactory()
            {
                Port = _rabbitMqSettings.Port,
                UserName = _rabbitMqSettings.Username,
                Password = _rabbitMqSettings.Password
            };
            _connection = _connectionFactory.CreateConnection();
        }

        #region SendEmailActivationSmtpMail
        public void SendEmailActivationSmtpEmail(EmailData model)
        {
            _channel = _connection.CreateModel();
            var props = _channel.CreateBasicProperties();
            props.Persistent = false;

            string exchangeName = "send-email-activation-smtp-email-direct";
            string routeKey = "route-send-email-activation";
            string queueName = "queue-send-email-activation-direct";

            _channel.ExchangeDeclare(exchange: exchangeName, durable: true, type: ExchangeType.Direct);
            _channel.QueueDeclare(queueName, true, false, false);
            _channel.QueueBind(queueName, exchangeName,routeKey,null);

            var emailJsonString = JsonSerializer.Serialize(model);
            _channel.BasicPublish(exchange: exchangeName, routingKey: routeKey, basicProperties:props, body: Encoding.UTF8.GetBytes(emailJsonString));

        }
        #endregion

    }
}
