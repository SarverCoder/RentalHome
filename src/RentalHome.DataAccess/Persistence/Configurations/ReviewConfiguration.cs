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
               .IsRequired();

        builder.Property(r => r.CreatedAt)
               .IsRequired();

        builder.HasOne(r => r.Booking)
            .WithOne(b => b.Review)
            .HasForeignKey<Review>(r => r.BookingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Property)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Tenant)
            .WithMany(tp => tp.PropertyReviews)
            .HasForeignKey(r => r.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Landlord)
            .WithMany(lp => lp.Reviews)
            .HasForeignKey(r => r.LandlordId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
