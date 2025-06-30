using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalHome.Core.Entities;

namespace RentalHome.DataAccess.Persistence.Configurations;

public class PropertyTypeConfiguration : IEntityTypeConfiguration<PropertyType>
{
    public void Configure(EntityTypeBuilder<PropertyType> builder)
    {
        builder.HasKey(p => p.Id);
          
        builder.Property(pt => pt.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(pt => pt.Description)
            .HasMaxLength(500);
            
        builder.Property(pt => pt.IconClass)
            .HasMaxLength(50);
    }
}