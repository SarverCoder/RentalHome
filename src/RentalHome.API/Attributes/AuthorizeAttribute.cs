using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using RentalHome.Application.Security.AuthEnums;

namespace RentalHome.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeAttribute : TypeFilterAttribute
{
    public AuthorizeAttribute()
        : base(typeof(AuthorizeFilter))
    {
        Arguments = [Array.Empty<ApplicationPermissionCode>()];
    }

    public AuthorizeAttribute(params ApplicationPermissionCode[] permissionCodes)
        : base(typeof(AuthorizeFilter))
    {
        Arguments = [permissionCodes];
    }

}