using RentalHome.API.Extensions;
using RentalHome.API.Middlewares;
using RentalHome.Application;
using RentalHome.Application.Services;
using RentalHome.Application.Services.Implementation;
using RentalHome.Application.Services;
using RentalHome.DataAccess;
using RentalHome.Infrastructure.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

#if !DEBUG
// Middleware & background service uchun kerak
builder.Services.AddHttpContextAccessor();
//builder.Services.AddTransient<LoggingMiddleware>();
builder.Services.AddHostedService<RabbitMQConsumer>();
#else

builder.Services.AddSwagger();  
builder.Services.AddMinIo(builder.Configuration);
builder.Services.AddSerilogMonitoring(builder.Configuration);


builder.Services.AddApplication(builder.Environment, builder.Configuration)
                .AddDataAccess(builder.Configuration);


builder.Services.AddAuth(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
#endif


var app = builder.Build();

//Seed roles and permissions on application startup
using (var scope = app.Services.CreateScope())
{
    var dataSeedService = scope.ServiceProvider.GetRequiredService<IDataSeedService>();
    await dataSeedService.SeedRolesAndPermissionsAsync();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseMiddleware<GlobalExceptionMiddleware>();
#if !DEBUG
app.UseMiddleware<LoggingMiddleware>();
#else
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
#endif
app.MapControllers();

app.Run();
