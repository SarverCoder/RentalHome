using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Helpers.GenerateJwt;
using RentalHome.Application.Helpers.PasswordHashers;
using RentalHome.Application.Models;
using RentalHome.Application.Services.Implementation;
using RentalHome.Core.Entities;
using RentalHome.DataAccess.Persistence;

namespace RentalHome.Application.Services;

public class UserService(
    DatabaseContext context, 
    IPasswordHasher passwordHasher, 
    IJwtTokenHandler jwtTokenHandler,
    IAuthService authService,
    IOtpService otpService,
    IEmailService emailService
    ) : IUserService
{

    public async Task<ApiResult<string>> VerifyOtpAsync(OtpVerificationModel model)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user is null)
            return ApiResult<string>.Failure(new[] { "Foydalanuvchi topilmadi." });

        var otp = await otpService.GetLatestOtpAsync(user.Id, model.Code);
        if (otp is null || otp.ExpiredAt < DateTime.Now)
            return ApiResult<string>.Failure(new[] { "Kod noto‘g‘ri yoki muddati tugagan." });

        user.IsVerified = true;
        await context.SaveChangesAsync();

        return ApiResult<string>.Success("OTP muvaffaqiyatli tasdiqlandi.");
    }

    public async Task<ApiResult<string>> RegisterAsync( string email, string password, string phoneNumber, string userName,bool isAdminSite)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(e => e.Email == email);
        if (existingUser != null)
            return ApiResult<string>.Failure(new[] { "Email allaqachon mavjud" });
       

        var salt = Guid.NewGuid().ToString();
        var hash = passwordHasher.Encrypt(password, salt);

        var user = new User()
        {
            
            UserName = userName,
            Email = email,
            PasswordHash = hash,
            PasswordSalt = salt,
            PhoneNumber = phoneNumber,
            IsActive = true
            

        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

       

        // --- Rolni isAdminSite ga qarab belgilash ---
        string roleName = isAdminSite ? "Admin" : "User";
        var defaultRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

        if (defaultRole == null)
        {
            // Agar kerakli rol topilmasa, xato qaytaramiz
            return ApiResult<string>.Failure(new[] { $"Tizimda '{roleName}' roli topilmadi. Admin bilan bog'laning." });
        }

        context.UserRoles.Add(new UserRole
        {
            UserId = user.Id,
            RoleId = defaultRole.Id
        });
        await context.SaveChangesAsync();
        // --- Rolni belgilash qismi tugadi ---

        var otp = await otpService.GenerateAndSaveOtpAsync(user.Id);
        await emailService.SendOtpAsync(email, otp);

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
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList(),
            Permissions = user.UserRoles
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(p => p.Permission.ShortName)
                .Distinct()
                .ToList()

        });

    }

    public async Task<ApiResult<UserAuthResponseModel>> GetUserAuth()
    {
        if (authService.User == null)
        {
            return ApiResult<UserAuthResponseModel>.Failure(new List<string> { "User not found" });
        }

        UserAuthResponseModel userPermissions = new UserAuthResponseModel
        {
            Id = authService.User.Id,
            FullName = authService.User.FullName,
            Permissions = authService.User.Permissions
        };

        return ApiResult<UserAuthResponseModel>.Success(userPermissions);
    }
}

