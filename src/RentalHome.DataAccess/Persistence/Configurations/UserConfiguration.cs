using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalHome.Core.Entities;
using System.Security.Cryptography;
using System.Text;

namespace RentalHome.DataAccess.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.Property(p => p.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(p => p.UserName)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.PasswordSalt)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true);


        //builder.HasData(GetSeedUser());

    }

    /// <summary>
    /// Parolni hashlash metod. Faqat `HasData` uchun maxsus.
    /// </summary>
    private static string Encrypt(string password, string salt)
    {
        // Rfc2898DeriveBytes - xavfsiz parol hashlash algoritmi
        using var algorithm = new Rfc2898DeriveBytes(
            password: password,
            salt: Encoding.UTF8.GetBytes(salt), // Saltni baytlarga aylantiramiz
            iterations: 10000, // Iteratsiyalar soni. Kamida 10000 yoki undan ko'proq tavsiya etiladi.
            hashAlgorithm: HashAlgorithmName.SHA256); // Hash algoritmi (SHA256)

        return Convert.ToBase64String(algorithm.GetBytes(32)); // 32 baytli hash hosil qilamiz va Base64 ga o'tkazamiz
    }

    /// <summary>
    /// Default SuperAdmin foydalanuvchisini yaratadi.
    /// </summary>
    private static User GetSeedUser()
    {
        // ID ni int turiga moslab to'g'riladim. Sizning IdConst klassingizni ham int ga o'zgartirishingiz kerak.
        const int seedUserId = 1; // SuperAdmin uchun ID, int turida
        const string seedSalt = "9f7d6dc5-34b4-4b66-a65e-0dc2fc17c0db"; // Bu yerda statik salt
        const string seedPassword = "web@1234"; // SuperAdminning boshlang'ich paroli

        return new User
        {
            Id = seedUserId, // IdConst.IdUserConst.SuperAdminId ni shu int qiymatiga moslang
            Fullname = "Adminjon", // Sizning User entity'ngizda 'Fullname'
            PhoneNumber = "+998934548544",
            UserName = "LixavCoder",
            Email = "superadmin@example.com",
            PasswordSalt = seedSalt,
            PasswordHash = Encrypt(seedPassword, seedSalt),
            CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            IsVerified = true // SuperAdmin odatda tasdiqlangan bo'ladi
        };
    }

}
