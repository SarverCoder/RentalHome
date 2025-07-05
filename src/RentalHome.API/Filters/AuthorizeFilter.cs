using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RentalHome.Application.Security.AuthEnums;
using RentalHome.Application.Services;

namespace RentalHome.API.Filters;

public class AuthorizeFilter : IAsyncAuthorizationFilter, IFilterMetadata
{
    private readonly ApplicationPermissionCode[] _permissionCodes;

    public AuthorizeFilter(ApplicationPermissionCode[] permissionCodes)
    {
        _permissionCodes = permissionCodes;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.Filters.Any(f => f is AllowAnonymousAttribute))
        {
            return;
        }

        var authService = context.HttpContext.RequestServices.GetService<IAuthService>();

        if (authService == null || !authService.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (_permissionCodes.Length > 0 && !_permissionCodes.All(p => authService.HasPermission(p.ToString())))
        {
            context.Result = new ForbidResult();
        }

    }

}