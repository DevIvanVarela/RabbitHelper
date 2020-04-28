using RabbitHelper.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RabbitHelper.Queues
{
    public class QueueHelper
    {
        public static void DeclareQueues(IModel channel)
        {
            foreach (var queue in Assembly.GetEntryAssembly().GetTypes().Where(mytype => mytype.GetInterfaces().Contains(typeof(IEvent))).Select(x => x.Name))
                channel.QueueDeclare(queue, true, false, false, null);
        }

        public static void DeclareQueues(IModel channel, bool durable, bool exclusive, bool autoDelete, IDictionary<string, object> arguments)
        {
            foreach (var queue in Assembly.GetEntryAssembly().GetTypes().Where(mytype => mytype.GetInterfaces().Contains(typeof(IEvent))).Select(x => x.Name))
                channel.QueueDeclare(queue, durable, exclusive, autoDelete, arguments);
        }
    }
}
