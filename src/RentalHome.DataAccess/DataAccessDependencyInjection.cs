using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;
using System;

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

    // ⚙️ Boshlang'ich admin rol va permissionsni qo'shish uchun method
    public static async Task SeedAdminRolePermissionsAsync(DatabaseContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        // Admin roli ID
        const int AdminRoleId = 1;
        //const int SuperAdminUserId = 1;

        // SuperAdmin oldin biriktirilganmi tekshiramiz
        var hasAdminRole = await context.UserRoles
            .AnyAsync(ur => ur.RoleId == AdminRoleId);

        if (!hasAdminRole)
        {
            context.UserRoles.Add(new UserRole
            {
                RoleId = AdminRoleId,
                UserId = 1
            });
            await context.SaveChangesAsync();
        }

        // Admin roliga biriktirilmagan permissionlar topiladi
        var missingPermissions = await context.Permissions
            .Where(p => !context.RolePermissions
                .Where(rp => rp.RoleId == AdminRoleId)
                .Select(rp => rp.PermissionId)
                .Contains(p.Id))
            .ToListAsync();

        if (missingPermissions.Any())
        {
            var newRolePermissions = missingPermissions.Select(p =>
                new RolePermission
                {
                    RoleId = AdminRoleId,
                    PermissionId = p.Id
                });

            await context.RolePermissions.AddRangeAsync(newRolePermissions);
            await context.SaveChangesAsync();
        }
    }

}
