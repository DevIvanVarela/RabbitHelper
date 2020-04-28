using RabbitHelper.Events;

namespace RabbitHelper.Consumers
{
    public interface IObjectConsumer<T> : IConsumer where T : class, IEvent
    {
        public void Consume(T message);
    }
}
