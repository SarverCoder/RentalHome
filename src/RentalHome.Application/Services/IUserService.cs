using RentalHome.Application.Models;
using RentalHome.Application.Models.Token;
using RentalHome.Application.Models.User;

namespace RentalHome.Application.Services;

public interface IUserService
{
    Task<ApiResult<string>> RegisterLandlordAsync(RegisterLandlordModel model);
    Task<ApiResult<string>> RegisterTenantAsync(RegisterTenantModel model);
    Task<ApiResult<string>> VerifyOtpAsync(OtpVerificationModel model);
    Task<ApiResult<LoginResponseModel>> LoginAsync(LoginUserModel model);
    Task<ApiResult<UserAuthResponseModel>> GetUserAuth();
    Task<ApiResult<LoginResponseModel>> RefreshTokenAsync(RefreshTokenRequestModel model);
    Task<ApiResult<string>> LogoutAsync(int userId);

}
