using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using HiringChallange.Application.Interfaces.MessageBrokers;

namespace HiringChallange.Infrastructure.Services.MessageBrokers
{
    public class RabbitmqService : IRabbitmqService
    {
        private readonly IRabbitmqConnection _connection;
        public RabbitmqService(IRabbitmqConnection connection) => _connection = connection;

        public void Publish<T>(T value, string exchangeType, string exchangeName, string queueName, string? routeKey)
        {
            using var connection = _connection.GetRabbitMqConnection();

            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchangeName, exchangeType, false, false);

            channel.QueueDeclare(queueName, false, false, false);

            channel.QueueBind(queueName, exchangeName, routeKey);

            var message = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value));

            channel.BasicPublish(exchangeName, routeKey, null, message);
        }
    }
}
