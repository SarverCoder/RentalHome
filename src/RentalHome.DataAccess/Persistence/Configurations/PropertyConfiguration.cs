using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalHome.Core.Entities;
namespace RentalHome.DataAccess.Persistence.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Title)
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(2000);
        
        builder.Property(p => p.AllowsPets)
            .HasDefaultValue(false);

        builder.Property(p => p.IsFurnished)
            .HasDefaultValue(false);
        
        
        builder
            .HasOne(p => p.Landlord)
            .WithMany(l => l.Properties)
            .HasForeignKey(p => p.LandlordId).IsRequired();
        
        builder
            .HasOne(p => p.PropertyType)
            .WithMany(p => p.Properties)
            .HasForeignKey(p => p.PropertyTypeId);
        
        builder
            .HasOne(p =>p.District)
            .WithMany(d => d.Properties)
            .HasForeignKey(p => p.DistrictId).IsRequired();
        
        
        
    }
}