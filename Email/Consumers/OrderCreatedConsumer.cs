using Email.Services;
using MessageBus.Dtos;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Email.Consumers
{
    public class OrderCreatedConsumer : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private IModel channel;
        private IConnection connection;
        private string queueName = string.Empty;

        public OrderCreatedConsumer(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            var orderCreatedTopic = configuration.GetValue<string>("MessageBusQueuesAndTopics:OrderCreatedTopic");

            channel.ExchangeDeclare(
                orderCreatedTopic, ExchangeType.Fanout);

            queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, orderCreatedTopic, string.Empty);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                await HandleMessage(JsonConvert.DeserializeObject<OrderCreatedMessageDto>(content));
                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume("auth-registration", false, consumer);
            return Task.CompletedTask;
        }

        private async Task HandleMessage(OrderCreatedMessageDto orderCreatedMessageDto)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                await emailService.SentEmail($"{orderCreatedMessageDto.UserId} placed order", "sample@gmail.com");
            }
        }
    }
}