using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RabbitHelper.Consumers
{
    public class ConsumerHelper
    {
        public static void RegisterConsumers(IModel channel)
        {
            foreach (var method in GetConsumerMethods())
            {
                var parameter = method.GetParameters().FirstOrDefault();

                var basicConsumer = new EventingBasicConsumer(channel);
                basicConsumer.Received += ConsumerReceived;
                channel.BasicConsume(parameter.ParameterType.Name, true, basicConsumer);
            }
        }

        private static IEnumerable<MethodInfo> GetConsumerMethods()
        {
            return Assembly
                .GetEntryAssembly()
                .GetTypes()
                .Where(mytype => mytype.GetInterfaces().Contains(typeof(IConsumer)) && mytype.IsClass)
                .SelectMany(x => x.GetMethods().Where(x => x.Name == "Consume"));
        }

        private static void ConsumerReceived(object sender, BasicDeliverEventArgs e)
        {
            foreach (var method in GetConsumerMethods())
            {
                var parameter = method.GetParameters().FirstOrDefault();
                if (e.RoutingKey != parameter.ParameterType.Name)
                    return;

                var methodParameter = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(e.Body), parameter.ParameterType);
                method.Invoke(method.DeclaringType.Assembly.CreateInstance(method.DeclaringType.FullName), new List<object> { methodParameter }.ToArray());
            }
        }
    }
}
