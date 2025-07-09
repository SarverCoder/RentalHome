using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Minio;
using RentalHome.Application.Common;
using Serilog;

namespace RentalHome.API.Extensions;

public static class Extensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            // API haqida umumiy ma'lumotlar
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Secure Login API",
                Version = "v1",
                Description = "SecureLoginApp uchun API hujjatlari",
                Contact = new OpenApiContact
                {
                    Name = "Your Name",
                    Email = "your.email@example.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Your License Name",
                    Url = new Uri("https://example.com/license") // URI xatosi tuzatildi!
                }
            });

            // JWT Bearer autentifikatsiyasini qo'shish
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {your token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            // JWT Bearer uchun global xavfsizlik talabini qo'shish
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2", // Bu shart emas, lekin qoldirilsa zarar qilmaydi
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>() // Bu yerda scope'lar bo'lishi mumkin, hozir bo'sh
                    }
                });

            // Agar sizda XML sharh fayllari bo'lsa
            // var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            // c.IncludeXmlComments(xmlPath);
        });

        
    }

    public static void AddMinIo(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioSettings>(configuration.GetSection("MinioSettings"));
        
        services.AddSingleton<IMinioClient>(sp =>
        {
            var minioSettings = sp.GetRequiredService<IOptions<MinioSettings>>().Value;

            // MinioClient obyektini yaratish
            var client = new MinioClient()
                .WithEndpoint(minioSettings.Endpoint)
                .WithCredentials(minioSettings.AccessKey, minioSettings.SecretKey);

            // Agar SSL yoqilgan bo'lsa
            if (minioSettings.UseSsl)
            {
                client = client.WithSSL();
            }

            return client.Build(); // MinioClient ni qurish
        });

    }

    public static void AddSerilogMonitoring(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog((serviceProvider, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom
                .Configuration(configuration);
        });

        

        
    }
}