using System.Text;

namespace NMHTestProject.Services
{
    public class QueueReaderBackgroundService : BackgroundService
    {
        private readonly ICalculationMessagingService _messagingService;
        private readonly ILogger<QueueReaderBackgroundService> _logger;

        public QueueReaderBackgroundService(
            ICalculationMessagingService messagingService,
            ILogger<QueueReaderBackgroundService> logger)
        {
            _messagingService = messagingService;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messagingService.Consume((model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation("Received {0}", message);
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
