using RabbitHelper.Events;
using RabbitMQ.Client;

namespace RabbitHelper.Publishers
{
    public interface IPublisher
    {
        void Publish<T>(T message) where T : IEvent;
        void Publish<T>(T message, string exchange) where T : IEvent;
        void Publish<T>(T message, string exchange, IBasicProperties basicProperties) where T : IEvent;
    }
}
