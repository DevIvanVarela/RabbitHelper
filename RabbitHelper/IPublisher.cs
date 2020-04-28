namespace RabbitHelper
{
    public interface IPublisher
    {
        void Publish<T>(T message) where T : IEvent;
    }
}
