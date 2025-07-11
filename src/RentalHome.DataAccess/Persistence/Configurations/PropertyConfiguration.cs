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
        
       
        
        
        builder
            .HasOne(p => p.Landlord)
            .WithMany(l => l.Properties)
            .HasForeignKey(p => p.LandlordId)
            .OnDelete(DeleteBehavior.Restrict);

        

        builder
            .HasOne(p =>p.District)
            .WithMany(d => d.Properties)
            .HasForeignKey(p => p.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);



    }
}