using Microsoft.EntityFrameworkCore;
using RentalHome.Application.Helpers.GenerateJwt;
using RentalHome.Application.Helpers.PasswordHashers;
using RentalHome.Application.Models;
using RentalHome.Application.Models.Landlord;
using RentalHome.Application.Models.Tenant;
using RentalHome.Application.Models.User;
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
    IEmailService emailService,
    ILandlordService landlordService,
    ITenantService tenantService
    ) : IUserService
{

    public async Task<ApiResult<string>> VerifyOtpAsync(OtpVerificationModel model)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user is null)
            return ApiResult<string>.Failure(new[] { "Foydalanuvchi topilmadi." });

        var otp = await otpService.GetLatestOtpAsync(user.Id, model.Code);
        if (otp is null || otp.ExpiredAt < DateTime.Now)
            return ApiResult<string>.Failure(new[] { "Kod noto�g�ri yoki muddati tugagan." });

        user.IsVerified = true;
        await context.SaveChangesAsync();

        return ApiResult<string>.Success("OTP muvaffaqiyatli tasdiqlandi.");
    }

    public async Task<ApiResult<string>> RegisterLandlordAsync(RegisterLandlordModel model)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(e => e.Email == model.Email);
        if (existingUser != null)
            return ApiResult<string>.Failure(new[] { "Email allaqachon mavjud" });

        var salt = Guid.NewGuid().ToString();
        var hash = passwordHasher.Encrypt(model.Password, salt);

        var user = new User()
        {
            UserName = model.UserName,
            Email = model.Email,
            PasswordHash = hash,
            PasswordSalt = salt,
            PhoneNumber = model.PhoneNumber,
            IsActive = true
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        // Role assignment
        string roleName = model.isAdminSite ? "Admin" : "User";
        var defaultRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

        if (defaultRole == null)
        {
            return ApiResult<string>.Failure(new[] { $"Tizimda '{roleName}' roli topilmadi. Admin bilan bog'laning." });
        }

        context.UserRoles.Add(new UserRole
        {
            UserId = user.Id,
            RoleId = defaultRole.Id
        });
        await context.SaveChangesAsync();

        // Create Landlord
        var createLandlordModel = new CreateLandlordModel
        {
            UserId = user.Id,
            CompanyName = model.CompanyName,
            Bio = model.Bio,
            IsVerified = false
        };

        await landlordService.AddAsync(createLandlordModel);

        var otp = await otpService.GenerateAndSaveOtpAsync(user.Id);
        await emailService.SendOtpAsync(model.Email, otp);

        return ApiResult<string>.Success("Landlord ro'yxatdan o'tdi. Email orqali tasdiqlang.");
    }

    public async Task<ApiResult<string>> RegisterTenantAsync(RegisterTenantModel model)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(e => e.Email == model.Email);
        if (existingUser != null)
            return ApiResult<string>.Failure(new[] { "Email allaqachon mavjud" });

        var salt = Guid.NewGuid().ToString();
        var hash = passwordHasher.Encrypt(model.Password, salt);

        var user = new User()
        {
            UserName = model.UserName,
            Email = model.Email,
            PasswordHash = hash,
            PasswordSalt = salt,
            PhoneNumber = model.PhoneNumber,
            IsActive = true
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        // Role assignment
        string roleName = model.isAdminSite ? "Admin" : "User";
        var defaultRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

        if (defaultRole == null)
        {
            return ApiResult<string>.Failure(new[] { $"Tizimda '{roleName}' roli topilmadi. Admin bilan bog'laning." });
        }

        context.UserRoles.Add(new UserRole
        {
            UserId = user.Id,
            RoleId = defaultRole.Id
        });
        await context.SaveChangesAsync();

        // Create Tenant
        var createTenantModel = new CreateTenantModel
        {
            UserId = user.Id,
            Latitude = model.Latitude,
            Longitude = model.Longitude
        };

        await tenantService.CreateAsync(createTenantModel);

        var otp = await otpService.GenerateAndSaveOtpAsync(user.Id);
        await emailService.SendOtpAsync(model.Email, otp);

        return ApiResult<string>.Success("Tenant ro'yxatdan o'tdi. Email orqali tasdiqlang.");
    }


    public async Task<ApiResult<LoginResponseModel>> LoginAsync(LoginUserModel model)
    {
        var user = await context.Users.FirstOrDefaultAsync(e => e.Email == model.Email);

        if (user is null)
            return ApiResult<LoginResponseModel>.Failure(new[] { "Foydalanuvchi topilmadi" });

        if (!passwordHasher.Verify(user.PasswordHash, model.Password, user.PasswordSalt))
            return ApiResult<LoginResponseModel>.Failure(new[] { "Parol noto�g�ri" });

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

