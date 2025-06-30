using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalHome.Core.Entities;
namespace RentalHome.DataAccess.Persistence.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder
            .HasOne(p => p.Landlord)
            .WithMany(l => l.Properties)
            .HasForeignKey(p => p.LandlordId);
        
        builder
            .HasOne(p => p.PropertyType)
            .WithMany(p => p.Properties)
            .HasForeignKey(p => p.PropertyTypeId);
        
        builder
            .HasOne(p =>p.District)
            .WithMany(d => d.Properties)
            .HasForeignKey(p => p.DistrictId);
        
    }
}