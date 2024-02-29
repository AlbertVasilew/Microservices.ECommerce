using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace MessageBus
{
    public class RabbitMQSender : IMessageBusSender
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly IConnection connection;

        public RabbitMQSender()
        {
            connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            connection = connectionFactory.CreateConnection();
        }

        public void SendMessage(object message, string queue)
        {
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue, exclusive: false, autoDelete: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(string.Empty, queue, null, body);
        }

        public void SendExchangeMessage(object message, string exchange)
        {
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange, ExchangeType.Fanout, false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange, string.Empty, null, body);
        }
    }
}