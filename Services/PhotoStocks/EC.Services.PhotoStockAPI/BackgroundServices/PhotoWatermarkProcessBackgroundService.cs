using Core.CrossCuttingConcerns.Communication.RabbitMQClientServices;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using EC.Services.PhotoStockAPI.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Drawing;
using System.Text;
using System.Text.Json;

namespace EC.Services.PhotoStockAPI.BackgroundServices
{
    internal class PhotoWatermarkProcessBackgroundService : BackgroundService
    {
        private readonly PhotoWatermarkClientService _photoWatermarkClientService;
        private readonly IElasticSearchLogService _elasticSearchLogService;
        private IModel _channel;

        public PhotoWatermarkProcessBackgroundService(PhotoWatermarkClientService photoWatermarkClientService, IElasticSearchLogService elasticSearchLogService)
        {
            _photoWatermarkClientService = photoWatermarkClientService;
            _elasticSearchLogService = elasticSearchLogService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _photoWatermarkClientService.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(PhotoWatermarkClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }

        private Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var imageCreatedEvent = JsonSerializer.Deserialize<PhotoWatermarkEvent>(Encoding.UTF8.GetString(@event.Body.ToArray()));
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", imageCreatedEvent.ImageName);
                string siteName = "www.ecproject-mysite-test.blabla";
                using var img = Image.FromFile(path);
                using var graphic = Graphics.FromImage(img);
                var font = new Font(FontFamily.GenericSansSerif, 40, FontStyle.Bold, GraphicsUnit.Pixel);
                var textSize = graphic.MeasureString(siteName, font);

                var color = Color.FromArgb(128, 255, 255, 255);
                var brush = new SolidBrush(color);
                var position = new Point(img.Width - ((int)textSize.Width + 30), img.Height - ((int)textSize.Height + 30));

                graphic.DrawString(siteName, font, brush, position);
                img.Save("wwwroot/photos/withwatermark/" + imageCreatedEvent.ImageName);
                img.Dispose();
                graphic.Dispose();

                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                var logDetail = new LogDetail()
                {
                    MethodName = "PhotoWatermarkProcessBackgroundService",
                    LoggingTime = DateTime.Now.ToString(),
                    Explanation = ex.InnerException?.Message ?? ex.Message,
                    Risk = (int)LogDetailRisks.Normal
                };
                _elasticSearchLogService.AddAsync(logDetail);
            }

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

    }
}
