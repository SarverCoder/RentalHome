using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalHome.Core.Entities;

namespace RentalHome.DataAccess.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);
        
        builder
            .HasOne(b =>b.Property)
            .WithMany(p => p.Bookings)
            .HasForeignKey(b => b.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(b =>b.Tenant)
            .WithMany(t => t.Bookings)
            .HasForeignKey(b => b.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(b => b.Landlord)
            .WithMany(l => l.Bookings)
            .HasForeignKey(b => b.LandlordId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}