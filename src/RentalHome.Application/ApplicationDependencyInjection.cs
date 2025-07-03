using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalHome.Application.MappingProfiles;
using RentalHome.Application.Services;
using RentalHome.Application.Services.Implementation;

namespace RentalHome.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
    {
        services.AddServices(env);

        services.RegisterAutoMapper();

        return services;
    }

    private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
    {
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
}
