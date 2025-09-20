using Microsoft.EntityFrameworkCore;
using RentalHome.API.Extensions;
using RentalHome.API.Middlewares;
using RentalHome.Application;
using RentalHome.Application.Services;
using RentalHome.DataAccess;
using RentalHome.DataAccess.Persistence;
using RentalHome.Infrastructure.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

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



var app = builder.Build();


var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
await context.Database.MigrateAsync();


using (var scope = app.Services.CreateScope())
{
    await AutomatedMigration.MigrateAsync(scope.ServiceProvider);
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

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
