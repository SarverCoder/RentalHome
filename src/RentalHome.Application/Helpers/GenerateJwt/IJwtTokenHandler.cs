using RentalHome.Core.Entities;
using System.Security.Claims;

namespace RentalHome.Application.Helpers.GenerateJwt;

public interface IJwtTokenHandler
{
    string GenerateAccessToken(User user, string sessionToken);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}