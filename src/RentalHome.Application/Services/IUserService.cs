using RentalHome.Application.Models;

namespace RentalHome.Application.Services;

public interface IUserService
{
    Task<ApiResult<string>> RegisterAsync( string email, string password, string phoneNumber,string userName, bool IsAdminSite);

    Task<ApiResult<LoginResponseModel>> LoginAsync(LoginUserModel model);
    Task<ApiResult<UserAuthResponseModel>> GetUserAuth();
}
