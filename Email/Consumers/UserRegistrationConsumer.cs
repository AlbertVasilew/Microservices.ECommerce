using Email.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Email.Consumers
{
    public class UserRegistrationConsumer : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private IModel channel;
        private IConnection connection;

        public UserRegistrationConsumer(IConfiguration configuration, IServiceProvider serviceProvider)
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

            channel.QueueDeclare(
                configuration.GetValue<string>("MessageBusQueues:AuthRegistration"),
                exclusive: false,
                autoDelete: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                await HandleMessage(JsonConvert.DeserializeObject<string>(content));
                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume("auth-registration", false, consumer);
            return Task.CompletedTask;
        }

        private async Task HandleMessage(string email)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                await emailService.SentEmail($"{email} registered", email);
            }
        }
    }
}