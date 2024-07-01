using NMHTestProject.Dto;
using RabbitMQ.Client.Events;

namespace NMHTestProject.Services
{
    public interface ICalculationMessagingService : IDisposable
    {
        void QueueCalculation(CalculationOutput calculationOutput);

        void Consume(EventHandler<BasicDeliverEventArgs> handler);
    }
}