using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Helpers.GenerateJwt;
using RentalHome.Application.Helpers.PasswordHashers;
using RentalHome.Application.Models;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services;

public class UserService(
    DatabaseContext context, 
    IPasswordHasher passwordHasher, 
    IJwtTokenHandler jwtTokenHandler
    ) : IUserService
{

    

    public async Task<ApiResult<string>> RegisterAsync(string firstName, string lastName, string email, string password, string phoneNumber)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(e => e.Email == email);
        if (existingUser != null)
            return ApiResult<string>.Failure(new[] { "Email allaqachon mavjud" });

        var salt = Guid.NewGuid().ToString();
        var hash = passwordHasher.Encrypt(password, salt);

        var user = new User()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PasswordHash = hash,
            PhoneNumber = phoneNumber,
            IsActive = true
            

        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return ApiResult<string>.Success("Ro'yxatdan o'tdingiz. Email orqali tasdiqlang.");


    }

    public async Task<ApiResult<LoginResponseModel>> LoginAsync(LoginUserModel model)
    {
        var user = await context.Users.FirstOrDefaultAsync(e => e.Email == model.Email);

        if (user is null)
            return ApiResult<LoginResponseModel>.Failure(new[] { "Foydalanuvchi topilmadi" });

        if (!passwordHasher.Verify(user.PasswordHash, model.Password, user.PasswordSalt))
            return ApiResult<LoginResponseModel>.Failure(new[] { "Parol noto‘g‘ri" });

        if (!user.IsActive)
            return ApiResult<LoginResponseModel>.Failure(new[] { "Email tasdiqlanmagan" });

        var accessToken = jwtTokenHandler.GenerateAccessToken(user, Guid.NewGuid().ToString());
        var refreshToken = jwtTokenHandler.GenerateRefreshToken();


        return ApiResult<LoginResponseModel>.Success(new LoginResponseModel
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            
        });

    }
}

