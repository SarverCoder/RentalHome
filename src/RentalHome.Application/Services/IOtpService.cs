using RentalHome.Core.Entities;

namespace RentalHome.Application.Services;

public interface IOtpService
{
    Task<string> GenerateAndSaveOtpAsync(int userId);
    Task<UserOTPs?> GetLatestOtpAsync(int userId, string code);
}