using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalHome.Core.Entities;

namespace RentalHome.DataAccess.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}