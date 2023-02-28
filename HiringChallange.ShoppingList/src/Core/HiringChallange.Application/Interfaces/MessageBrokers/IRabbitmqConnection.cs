using RabbitMQ.Client;

namespace HiringChallange.Application.Interfaces.MessageBrokers
{
    public interface IRabbitmqConnection
    {
        IConnection GetRabbitMqConnection();
    }
}
