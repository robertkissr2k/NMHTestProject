using Microsoft.Extensions.Options;
using NMHTestProject.Common;
using NMHTestProject.Dto;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NMHTestProject.Services
{
    public class CalculationMessagingService : ICalculationMessagingService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IOptions<MessagingConfiguration> _configuration;
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public CalculationMessagingService(IOptions<MessagingConfiguration> configuration)
        {
            _configuration = configuration;
            MessagingConfiguration config = _configuration.Value;
            _connectionFactory = new ConnectionFactory
            {
                UserName = config.UserName,
                Password = config.Password.ToString(),
                //VirtualHost = config.VirtualHost,
                HostName = config.HostName,
                Port = config.Port,
                ClientProvidedName = config.ClientProviderName
            };

            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: config.QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void QueueCalculation(CalculationOutput calculationOutput)
        {
            MessagingConfiguration config = _configuration.Value;

            string message = JsonSerializer.Serialize(calculationOutput);
            byte[] body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                 routingKey: config.QueueName,
                                 basicProperties: null,
                                 body: body);
        }

        public void Consume(EventHandler<BasicDeliverEventArgs> handler)
        {
            MessagingConfiguration config = _configuration.Value;
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += handler;

            _channel.BasicConsume(queue: config.QueueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }


    }
}
