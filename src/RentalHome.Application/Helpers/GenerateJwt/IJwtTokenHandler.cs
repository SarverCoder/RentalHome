using RentalHome.Core.Entities;

namespace RentalHome.Application.Helpers.GenerateJwt;

public interface IJwtTokenHandler
{
    string GenerateAccessToken(User user, string sessionToken);
    string GenerateRefreshToken();
}