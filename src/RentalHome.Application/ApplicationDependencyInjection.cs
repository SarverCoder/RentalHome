using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RentalHome.Application.Common;
using RentalHome.Application.Helpers.GenerateJwt;
using RentalHome.Application.Helpers.PasswordHashers;
using RentalHome.Application.Services;
using RentalHome.Application.Services.Implementation;
using System.Text;

namespace RentalHome.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
    {
        services.AddServices(env);
        
        services.RegisterAutoMapper();

        services.AddEmailConfiguration(configuration);

        services.AddOptions<JwtOption>()
            .BindConfiguration("JwtOption");

        return services;
    }

    private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<ILandlordService, LandlordService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IAmenityService, AmenityService>();
        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<IDistrictService, DistrictService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IFileStorageService, MinioFileStorageService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IOtpService, OtpService>();

        services.AddSingleton<IRabbitMQProducer, RabbitMQProducer>();



    }
    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IUserService));
        services.AddAutoMapper(typeof(ITenantService));
        services.AddAutoMapper(typeof(IPropertyService));
        services.AddAutoMapper(typeof(IPhotoService));
        services.AddAutoMapper(typeof(ILandlordService));
        services.AddAutoMapper(typeof(IBookingService));
        services.AddAutoMapper(typeof(IAmenityService));
        services.AddAutoMapper(typeof(IDistrictService));
        services.AddAutoMapper(typeof(IRegionService));


    }

    public static IServiceCollection AddAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection("JwtOption").Get<JwtOption>();

        if (jwtOptions == null)
        {
            throw new InvalidOperationException("JWT sozlamalari topilmadi. appsettings.json faylida 'JwtOption' bo'limini tekshiring.");
        }

        serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });

        return serviceCollection;
    }


    public static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
    }
}
