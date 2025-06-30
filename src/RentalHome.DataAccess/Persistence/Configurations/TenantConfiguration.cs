using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalHome.Core.Entities;

namespace RentalHome.DataAccess.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Latitude)
               .HasColumnType("decimal(9,6)")
               .IsRequired();

        builder.Property(t => t.Longitude)
               .HasColumnType("decimal(9,6)")
               .IsRequired();


        builder.HasOne(rp => rp.User)
               .WithOne(r => r.Tenant)
               .HasForeignKey<Tenant>(t => t.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tp => tp.PreferredPropertyType)
            .WithMany()
            .HasForeignKey(tp => tp.PreferredPropertyTypeId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
