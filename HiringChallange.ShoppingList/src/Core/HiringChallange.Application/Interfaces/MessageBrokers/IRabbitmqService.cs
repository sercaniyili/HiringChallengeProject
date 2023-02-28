namespace HiringChallange.Application.Interfaces.MessageBrokers
{
    public interface IRabbitmqService
    {
        void Publish<T>(T value, string exchangeType, string exchangeName, string queueName, string? routeKey);
    }
}
