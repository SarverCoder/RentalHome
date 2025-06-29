using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalHome.Core.Entities;

namespace RentalHome.DataAccess.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(p => p.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.LastName)
            .HasMaxLength(100)
            .IsRequired();

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


        

    }
}
