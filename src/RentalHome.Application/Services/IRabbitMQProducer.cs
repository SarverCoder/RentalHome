namespace RentalHome.Application.Services;

public interface IRabbitMQProducer
{
    Task SendMessageAsync(string message);
}
