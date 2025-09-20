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
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "JWT Authorization header using the Bearer scheme. \n\r Enter 'Bearer' [space] and then your token in the text input below.\n\r Example: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, []
                }
            });
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