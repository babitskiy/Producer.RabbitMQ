using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Producer.RabbitMQ
{
	public class RabbitMQProducer : IMessageProducer
	{
		public RabbitMQProducer()
		{
		}

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "orders",
                       durable: true,
                       exclusive: false,
                       autoDelete: false,
                       arguments: null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
        }
    }
}

