using Microsoft.EntityFrameworkCore;
using RentalHome.Core.Entities;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace RentalHome.DataAccess.Persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Logging> Logs { get; set; }
    public DbSet<UserOTPs> UserOTPs { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Landlord> Landlords { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionGroup> PermissionGroups { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyAmenity> PropertyAmenities { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

        SeedData(builder);
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

    private void SeedData(ModelBuilder modelBuilder)
    {
        // 1. Avval Roles
       /* modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "SuperAdmin", CreatedAt = DateTime.UtcNow },
            new Role { Id = 2, Name = "Admin", CreatedAt = DateTime.UtcNow },
            new Role { Id = 3, Name = "Landlord", CreatedAt = DateTime.UtcNow },
            new Role { Id = 4, Name = "Tenant", CreatedAt = DateTime.UtcNow }
        );
*/

        // ID ni int turiga moslab to'g'riladim. Sizning IdConst klassingizni ham int ga o'zgartirishingiz kerak.
        const int seedUserId = 1; // SuperAdmin uchun ID, int turida
        const string seedSalt = "9f7d6dc5-34b4-4b66-a65e-0dc2fc17c0db"; // Bu yerda statik salt
        const string seedPassword = "web@1234"; // SuperAdminning boshlang'ich paroli


        // 2. Keyin Users
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = seedUserId,
                Fullname = "Adminjon",
                PhoneNumber = "+998934548544",
                UserName = "LixavCoder",
                Email = "superadmin@example.com",
                PasswordHash = Encrypt(seedPassword,seedSalt),
                PasswordSalt = seedSalt,
                TokenExpiryTime = DateTime.MinValue,
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue,
                IsActive = true,
                IsVerified = true
            }
        );

        // 3. Eng oxirida UserRoles
        //modelBuilder.Entity<UserRole>().HasData(
        //    new UserRole { Id = 1, UserId = 1, RoleId = 1 }
        //);
    }
}
