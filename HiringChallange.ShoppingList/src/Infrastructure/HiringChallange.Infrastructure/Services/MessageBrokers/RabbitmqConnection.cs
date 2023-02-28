using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Application.Interfaces.MessageBrokers;

namespace HiringChallange.Infrastructure.Services.MessageBrokers
{
    public class RabbitmqConnection : IRabbitmqConnection
    {
        public IConnection GetRabbitMqConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                VirtualHost = "/",
                Port = 5672,
                UserName = "guest",
                Password = "guest"

            }.CreateConnection();

            return connectionFactory;
        }
    }
}
