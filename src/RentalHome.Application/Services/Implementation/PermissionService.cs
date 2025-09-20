using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Extensions;
using RentalHome.Application.Models;
using RentalHome.Application.Models.Permissions;
using RentalHome.Application.Security;
using RentalHome.Application.Security.AuthEnums;
using RentalHome.DataAccess.Persistence;
using System.Reflection;

namespace RentalHome.Application.Services.Implementation;

public class PermissionService : IPermissionService
{
    private readonly DatabaseContext _dbContext;
    public PermissionService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<PermissionCodeDescription> GetAllPermissionDescriptions()
    {
        var permissions = new List<PermissionCodeDescription>();

        foreach (var field in typeof(ApplicationPermissionCode).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var attribute = field.GetCustomAttribute<ApplicationPermissionDescriptionAttribute>();

            if (attribute != null)
            {
                permissions.Add(new PermissionCodeDescription
                {
                    Code = field.Name,
                    ShortName = attribute.ShortName,
                    FullName = attribute.FullName
                });
            }
        }
        return permissions;
    }

    public string GetPermissionShortName(ApplicationPermissionCode permissionCode)
    {
        FieldInfo? field = typeof(ApplicationPermissionCode).GetField(permissionCode.ToString());

        if (field != null)
        {
            var attribute = field.GetCustomAttribute<ApplicationPermissionDescriptionAttribute>();
            if (attribute != null)
            {
                return attribute.ShortName;
            }
        }

        return permissionCode.ToString();
    }

    public async Task<ApiResult<List<PermissionGroupListModel>>> GetPermissionsFromDbAsync()
    {
        // 1. Permissionlarni bazaga sinxronlash (qo'shish/yangilash)
        

        // 2. Bazadan barcha permissionlarni guruhlari bilan birga yuklash
        var permissions = await _dbContext.Permissions
            .Include(p => p.PermissionGroup)
            .AsNoTracking()
            .ToListAsync();

        // 3. Olingan permissionlarni DTOlarga map qilish va guruhlash
        var groupedPermissions = permissions
            .GroupBy(p => p.PermissionGroup.Name) // PermissionGroup.Name orqali guruhlash
            .Select(g => new PermissionGroupListModel
            {
                GroupName = g.Key,
                Permissions = g.Select(p => new PermissionListModel
                {
                    Id = p.Id,
                    ShortName = p.ShortName, // Permission.ShortName
                    FullName = p.FullName,   // Permission.FullName
                    GroupName = p.PermissionGroup.Name // PermissionGroup.Name
                }).OrderBy(p => p.ShortName).ToList()
            })
            .OrderBy(pg => pg.GroupName)
            .ToList();

        return ApiResult<List<PermissionGroupListModel>>.Success(groupedPermissions);
    }
}