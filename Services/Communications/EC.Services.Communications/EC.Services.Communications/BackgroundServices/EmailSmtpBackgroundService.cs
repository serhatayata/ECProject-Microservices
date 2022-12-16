using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Core.CrossCuttingConcerns.Logging;
using Core.Entities;
using Core.Extensions;
using EC.Services.Communications.Constants;
using EC.Services.Communications.Services.Abstract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Core.CrossCuttingConcerns.Communication.RabbitMQClientServices;

namespace EC.Services.Communications.BackgroundServices
{
    internal class EmailSmtpBackgroundService:BackgroundService
    {
        private readonly EmailSmtpClientService _clientService;
        private readonly IEmailService _emailService;
        private readonly IElasticSearchLogService _elasticSearchLogService;
        private IModel _channel;

        public EmailSmtpBackgroundService(EmailSmtpClientService clientService, IEmailService emailService, IElasticSearchLogService elasticSearchLogService)
        {
            _clientService = clientService;
            _emailService = emailService;
            _elasticSearchLogService = elasticSearchLogService;
        }

        #region StartAsync
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _clientService.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }
        #endregion
        #region ExecuteAsync
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(EmailSmtpClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var emailSmtpEventData = JsonSerializer.Deserialize<EmailData>(Encoding.UTF8.GetString(@event.Body.ToArray()));
                var emailSentResult = await _emailService.SendSmtpEmail(emailSmtpEventData);

                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                var emailSmtpEventData = JsonSerializer.Deserialize<EmailData>(Encoding.UTF8.GetString(@event.Body.ToArray()));

                var message = $"{MessageExtensions.NotSent(CommConstantValues.EmailSMTP)} - To : {emailSmtpEventData?.EmailToId} - Error : {ex.InnerException?.Message}";
                var logDetailError = LogExtensions.GetLogDetails(null, (int)LogDetailRisks.Normal, DateTime.Now.ToString(), message);

                await _elasticSearchLogService.AddAsync(logDetailError);
            }
        }
        #endregion
        #region StopAsync
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
        #endregion

    }
}
