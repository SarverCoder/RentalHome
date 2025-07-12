using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RentalHome.Application.Services;
using RentalHome.DataAccess.Persistence;
using System.Security.Claims;

namespace RentalHome.API.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRabbitMQProducer _producer;
    private readonly IServiceProvider _serviceProvider;

    public LoggingMiddleware(RequestDelegate next, IRabbitMQProducer producer, IServiceProvider serviceProvider)
    {
        _next = next;
        _producer = producer;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var user = context.User;
        var method = context.Request.Method;

        if (user.Identity?.IsAuthenticated == true &&
            (method == "POST" || method == "PUT" || method == "DELETE"))
        {
            var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == "Landlord")
            {
                var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var username = user.Identity?.Name ?? "Unknown";

                // Scoped DbContext olish (IServiceProvider orqali)
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                // LandlordId va qo‘shimcha ma’lumotlarni olish
                int.TryParse(userId, out int uid);
                var landlord = await dbContext.Landlords
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == uid);

                var log = new
                {
                    UserId = userId,
                    Email = email,
                    Username = username,
                    Role = role,
                    LandlordId = landlord?.Id,
                    CompanyName = landlord?.CompanyName,
                    Method = method,
                    Path = context.Request.Path,
                    Timestamp = DateTime.UtcNow,
                    IpAddress = context.Connection.RemoteIpAddress?.ToString()
                };

                var json = JsonConvert.SerializeObject(log);
                await _producer.SendMessageAsync(json);
            }
        }

        await _next(context);
    }
}