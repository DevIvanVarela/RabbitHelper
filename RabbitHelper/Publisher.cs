using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitHelper
{
    public class Publisher : IPublisher
    {
        public Publisher(IModel channel)
        {
            _channel = channel;
        }

        private readonly IModel _channel;

        public void Publish<T>(T message) where T : IEvent
        {
            _channel.BasicPublish(
                exchange: "",
                routingKey: typeof(T).Name,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
        }
    }
}