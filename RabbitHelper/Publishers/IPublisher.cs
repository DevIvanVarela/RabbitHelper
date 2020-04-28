using RabbitHelper.Events;

namespace RabbitHelper.Publishers
{
    public interface IPublisher
    {
        void Publish<T>(T message) where T : IEvent;
    }
}
