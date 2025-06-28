using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.DataAccess;

public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(connectionString,
                opt => opt.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));
    }
}
