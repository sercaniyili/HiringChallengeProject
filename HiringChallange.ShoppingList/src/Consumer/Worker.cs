using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;
using System.Text.Json;
using HiringChallange.Application.DTOs.ShoppingList;
using HiringChallange.Application.Interfaces.Contract;
using HiringChallange.Application.Interfaces.MessageBrokers;

namespace ShoppingListConsumer
{
    public class Worker : BackgroundService
    {
        public const string ShoppingListCollection = "CompletedLists";

        private readonly ILogger<Worker> _logger;
        private readonly IMongoConnect _mongoDbConnect;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IMongoConnect mongoConnect, IConfiguration configuration, IRabbitmqConnection connection)
        {
            _logger = logger;
            _mongoDbConnect = mongoConnect;
            _configuration = configuration;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var collection = _mongoDbConnect.ConnectToMongo<ShoppingListToBsonDto>(ShoppingListCollection);

            while (!stoppingToken.IsCancellationRequested)
            {

                var connectioFactory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    VirtualHost = "/",
                    Port = 5672,
                    UserName = "guest",
                    Password = "guest"
                };

                var connection = connectioFactory.CreateConnection();

                var channel = connection.CreateModel();

                channel.QueueDeclare("direct.queuName", false, false, false);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (sender, args) =>
                {
                    var message = JsonSerializer.Deserialize<ShoppingListToBsonDto>(Encoding.UTF8.GetString(args.Body.ToArray()));


                    await collection.InsertOneAsync(message);
                };

                channel.BasicConsume("direct.queuName", false, consumer);

            }
        }
    }
}