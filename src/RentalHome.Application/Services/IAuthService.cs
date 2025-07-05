using RentalHome.Application.Security;

namespace RentalHome.Application.Services;

public interface IAuthService
{
    IUser User { get; }
    int GetUserId();
    bool IsAuthenticated { get; }
    HashSet<string> Permissions { get; }
    bool HasPermission(params string[] permissionCodes);
}