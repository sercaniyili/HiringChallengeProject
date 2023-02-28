using ShoppingListConsumer;
using HiringChallange.Application.Interfaces.Contract;
using HiringChallange.Application.Interfaces.MessageBrokers;
using HiringChallange.Infrastructure.Services.MessageBrokers;
using HiringChallange.Persistence.Context;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<IMongoConnect, MongoDbConnect>();
        services.AddTransient<IRabbitmqConnection, RabbitmqConnection>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();


