using Newtonsoft.Json;
using RabbitHelper.Events;
using RabbitMQ.Client;
using System.Text;

namespace RabbitHelper.Publishers
{
    public class PublisherHelper : IPublisher
    {
        public PublisherHelper(IModel channel)
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

        public void Publish<T>(T message, string exchange) where T : IEvent
        {
            _channel.BasicPublish(
                exchange: exchange,
                routingKey: typeof(T).Name,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
        }

        public void Publish<T>(T message, string exchange, IBasicProperties basicProperties) where T : IEvent
        {
            _channel.BasicPublish(
                exchange: exchange,
                routingKey: typeof(T).Name,
                basicProperties: basicProperties,
                body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
        }
    }
}