using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalHome.Core.Entities;

namespace RentalHome.DataAccess.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Rating)
               .IsRequired();

        builder.Property(r => r.Comment)
               .HasMaxLength(1000)
               .IsRequired(false);

        builder.Property(r => r.ReviewType)
               .HasConversion<string>()
               .IsRequired();

        builder.Property(r => r.CreatedAt)
               .HasColumnType("timestamp")
               .IsRequired();

        builder.HasOne(r => r.Tenant)
               .WithMany()
               .HasForeignKey(r => r.TenantId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Landlord)
               .WithMany()
               .HasForeignKey(r => r.LandlordId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Property)
               .WithMany()
               .HasForeignKey(r => r.PropertyId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Booking)
               .WithMany()
               .HasForeignKey(r => r.BookingId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
