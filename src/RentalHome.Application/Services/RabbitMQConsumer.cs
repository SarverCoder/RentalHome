using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;
using System.Text;
using System.Text.Json;

namespace RentalHome.Infrastructure.Consumers;

public class RabbitMQConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<RabbitMQConsumer> _logger;
    private IConnection? _connection;
    private IModel? _channel;
    private readonly string _queueName;

    public RabbitMQConsumer(
        IConfiguration configuration,
        IServiceScopeFactory scopeFactory,
        ILogger<RabbitMQConsumer> logger)
    {
        _configuration = configuration;
        _scopeFactory = scopeFactory;
        _logger = logger;
        _queueName = _configuration["RabbitMQ:QueueName"] ?? "logging-queue";
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(2000, stoppingToken); // Ilova ishga tushishini kutamiz

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await StartConsumer(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RabbitMQ log consumer ishga tushirishda xatolik");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }

    private async Task StartConsumer(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:HostName"] ?? "localhost",
            UserName = _configuration["RabbitMQ:UserName"] ?? "guest",
            Password = _configuration["RabbitMQ:Password"] ?? "guest",
            Port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672")
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        // Fair dispatch - har bir consumer ga faqat bitta message
        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation("Xabar qabul qilindi: {Message}", message);


                var log = JsonSerializer.Deserialize<Logging>(message);

                if (log != null)
                {
                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                    db.Logs.Add(log);
                    await db.SaveChangesAsync();

                    _logger.LogInformation("Log saqlandi: {LogType} - {Path}", log.Type, log.Path);

                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                else
                {
                    _logger.LogWarning("Xatolik: Log obyekt deserializatsiya qilinmadi.");
                    _channel.BasicNack(ea.DeliveryTag, false, false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Log message ni qayta ishlashda xatolik");
                _channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };

        _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

        _logger.LogInformation("LoggingConsumerService ishga tushdi.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("LoggingConsumerService to‘xtatildi.");
        _channel?.Close();
        _connection?.Close();
        return base.StopAsync(cancellationToken);
    }

    public override void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }
}
