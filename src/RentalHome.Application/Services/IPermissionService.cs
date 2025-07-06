using RentalHome.Application.Models;
using RentalHome.Application.Models.Permissions;
using RentalHome.Application.Security;
using RentalHome.Application.Security.AuthEnums;

namespace RentalHome.Application.Services;

public interface IPermissionService
{
    List<PermissionCodeDescription> GetAllPermissionDescriptions();
    string GetPermissionShortName(ApplicationPermissionCode permissionCode);
    Task<ApiResult<List<PermissionGroupListModel>>> GetPermissionsFromDbAsync();
}