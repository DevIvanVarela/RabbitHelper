namespace RabbitHelper
{
    public interface IConsume<T> : IConsume where T : class, IEvent 
    {
        public void Consume(T message);
    }

    public interface IConsume
    {
    }
}
