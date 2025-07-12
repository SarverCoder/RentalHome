using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace RentalHome.Application.Services.Implementation;

public class RabbitMQProducer : IRabbitMQProducer
{
    private readonly RabbitMQ.Client.IModel _channel;

    public RabbitMQProducer(IConfiguration config)
    {
        var factory = new ConnectionFactory
        {
            HostName = config["RabbitMQ:HostName"] ?? "localhost",
            UserName = config["RabbitMQ:UserName"] ?? "guest",
            Password = config["RabbitMQ:Password"] ?? "guest"
        };

        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();

        _channel.QueueDeclare(
            queue: "logQueue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    public Task SendMessageAsync(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        var props = _channel.CreateBasicProperties();
        props.Persistent = true;

        _channel.BasicPublish(
            exchange: "",
            routingKey: "logQueue",
            basicProperties: props,
            body: body
        );

        return Task.CompletedTask;
    }
}