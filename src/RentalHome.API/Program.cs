using RentalHome.API.Extensions;
using Microsoft.EntityFrameworkCore;
using RentalHome.Application;
using RentalHome.DataAccess;
using RentalHome.DataAccess.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwagger();  


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


if (builder.Environment.IsProduction() && builder.Configuration.GetValue<int?>("POST") is not null)
    builder.WebHost.UseUrls($"http://*:{builder.Configuration.GetValue<int>("POST")}");


var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
await context.Database.MigrateAsync();



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
