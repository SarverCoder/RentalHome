using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentalHome.Application.Security;
using RentalHome.Application.Security.AuthEnums;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;
using System.Reflection;
using UserRoleEnum = RentalHome.Core.Enums.UserRole;

namespace RentalHome.Application.Services.Implementation;

public class DataSeedService : IDataSeedService
{
    private readonly DatabaseContext _context;
    private readonly ILogger<DataSeedService> _logger;

    public DataSeedService(DatabaseContext context, ILogger<DataSeedService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedRolesAndPermissionsAsync()
    {
        await SeedRolesAsync();
        await SeedPermissionGroupsAsync();
        await SeedPermissionsAsync();
        await SeedRolePermissionsAsync();
        await AssignAdminRoleToUserAsync("+998934548544");
    }

    private async Task SeedRolesAsync()
    {
        _logger.LogInformation("Starting roles seeding...");

        // Get all enum values from UserRole
        var roleEnumValues = Enum.GetValues<UserRoleEnum>();

        foreach (var roleEnum in roleEnumValues)
        {
            var roleId = (int)roleEnum;
            var roleName = roleEnum.ToString();

            // Check if role exists
            var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            if (existingRole == null)
            {
                // Create new role
                var newRole = new Role
                {
                    Id = roleId,
                    Name = roleName,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Roles.AddAsync(newRole);
                _logger.LogInformation($"Added new role: {roleName}");
            }
            else if (existingRole.Name != roleName)
            {
                // Update role name if changed
                existingRole.Name = roleName;
                existingRole.UpdatedAt = DateTime.UtcNow;
                _logger.LogInformation($"Updated role name: {roleName}");
            }
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Roles seeding completed.");
    }

    private async Task SeedPermissionGroupsAsync()
    {
        _logger.LogInformation("Starting permission groups seeding...");

        var permissionGroupEnums = Enum.GetValues<ApplicationPermissionGroupCode>();

        foreach (var groupEnum in permissionGroupEnums)
        {
            var groupId = (int)groupEnum;
            var groupName = groupEnum.ToString();

            var existingGroup = await _context.PermissionGroups.FirstOrDefaultAsync(pg => pg.Id == groupId);

            if (existingGroup == null)
            {
                var newGroup = new PermissionGroup
                {
                    Id = groupId,
                    Name = groupName,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.PermissionGroups.AddAsync(newGroup);
                _logger.LogInformation($"Added new permission group: {groupName}");
            }
            else if (existingGroup.Name != groupName)
            {
                existingGroup.Name = groupName;
                existingGroup.UpdatedAt = DateTime.UtcNow;
                _logger.LogInformation($"Updated permission group name: {groupName}");
            }
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Permission groups seeding completed.");
    }

    private async Task SeedPermissionsAsync()
    {
        _logger.LogInformation("Starting permissions seeding...");

        var permissionEnums = Enum.GetValues<ApplicationPermissionCode>();

        foreach (var permissionEnum in permissionEnums)
        {
            var permissionId = (int)permissionEnum;
            var permissionShortName = permissionEnum.ToString();

            // Get permission description from attribute
            var memberInfo = typeof(ApplicationPermissionCode).GetMember(permissionEnum.ToString()).FirstOrDefault();
            var attribute = memberInfo?.GetCustomAttribute<ApplicationPermissionDescriptionAttribute>();

            if (attribute == null) continue;

            var permissionGroupId = (int)attribute.ModulePermissionGroup;
            var permissionFullName = attribute.FullName;

            var existingPermission = await _context.Permissions.FirstOrDefaultAsync(p => p.Id == permissionId);

            if (existingPermission == null)
            {
                var newPermission = new Permission
                {
                    Id = permissionId,
                    ShortName = permissionShortName,
                    FullName = permissionFullName,
                    PermissionGroupId = permissionGroupId,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Permissions.AddAsync(newPermission);
                _logger.LogInformation($"Added new permission: {permissionShortName}");
            }
            else
            {
                // Update if changed
                bool isUpdated = false;
                if (existingPermission.ShortName != permissionShortName)
                {
                    existingPermission.ShortName = permissionShortName;
                    isUpdated = true;
                }
                if (existingPermission.FullName != permissionFullName)
                {
                    existingPermission.FullName = permissionFullName;
                    isUpdated = true;
                }
                if (existingPermission.PermissionGroupId != permissionGroupId)
                {
                    existingPermission.PermissionGroupId = permissionGroupId;
                    isUpdated = true;
                }

                if (isUpdated)
                {
                    existingPermission.UpdatedAt = DateTime.UtcNow;
                    _logger.LogInformation($"Updated permission: {permissionShortName}");
                }
            }
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Permissions seeding completed.");
    }

    private async Task SeedRolePermissionsAsync()
    {
        _logger.LogInformation("Starting role-permission assignments...");

        // Define role-permission mappings  
        var rolePermissions = new Dictionary<int, ApplicationPermissionCode[]>();

        rolePermissions.Add((int)UserRoleEnum.Admin, Enum.GetValues<ApplicationPermissionCode>());

        rolePermissions.Add((int)UserRoleEnum.Seller, new[]
        {
            ApplicationPermissionCode.UserRead,
            ApplicationPermissionCode.GetProperty,
            ApplicationPermissionCode.GetProperties,
            ApplicationPermissionCode.CreateProperty,
            ApplicationPermissionCode.UpdateProperty,
            ApplicationPermissionCode.DeleteProperty,
            ApplicationPermissionCode.GetDistricts,
            ApplicationPermissionCode.GetRegions,
            ApplicationPermissionCode.GetAmenities,
            ApplicationPermissionCode.GetBooking,
            ApplicationPermissionCode.GetBookings,
        });

        rolePermissions.Add((int)UserRoleEnum.Tenant, new[]
        {
            ApplicationPermissionCode.UserRead,
            ApplicationPermissionCode.GetProperty,
            ApplicationPermissionCode.GetProperties,
            ApplicationPermissionCode.GetDistricts,
            ApplicationPermissionCode.GetRegions,
            ApplicationPermissionCode.GetAmenities,
            ApplicationPermissionCode.CreateBooking,
            ApplicationPermissionCode.GetBookings,
            ApplicationPermissionCode.DeleteBooking
        });

        foreach (var rolePermission in rolePermissions)
        {
            var roleId = rolePermission.Key;
            var permissions = rolePermission.Value;

            foreach (var permission in permissions)
            {
                var permissionId = (int)permission;

                // Check if role-permission exists
                var existingRolePermission = await _context.RolePermissions
                    .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

                if (existingRolePermission == null)
                {
                    var newRolePermission = new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permissionId
                    };

                    await _context.RolePermissions.AddAsync(newRolePermission);
                    _logger.LogInformation($"Added permission {permission} to role {roleId}");
                }
            }
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Role-permission assignments completed.");
    }

    public async Task AssignAdminRoleToUserAsync(string adminPhoneNumber)
    {
        var adminUser = await _context.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == adminPhoneNumber);

        if (adminUser == null)
        {
            _logger.LogWarning("Admin user not found by phone number.");
            return;
        }

        var adminRole = await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == nameof(UserRoleEnum.Admin));

        if (adminRole == null)
        {
            _logger.LogWarning("Admin role not found.");
            return;
        }

        var alreadyAssigned = await _context.UserRoles
            .AnyAsync(ur => ur.UserId == adminUser.Id && ur.RoleId == adminRole.Id);

        if (!alreadyAssigned)
        {
            var userRole = new UserRole
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id
            };

            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Admin role assigned to the user.");
        }
        else
        {
            _logger.LogInformation("Admin user already has the Admin role.");
        }
    }
}