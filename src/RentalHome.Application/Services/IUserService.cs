using RentalHome.Application.Models;

namespace RentalHome.Application.Services;

public interface IUserService
{
    Task<ApiResult<string>> RegisterAsync(string firstName, string lastName, string email, string password, string phoneNumber);

    Task<ApiResult<LoginResponseModel>> LoginAsync(LoginUserModel model);

}
