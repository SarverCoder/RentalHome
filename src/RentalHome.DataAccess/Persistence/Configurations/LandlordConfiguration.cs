using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalHome.Core.Entities;

namespace RentalHome.DataAccess.Persistence.Configurations;

public class LandlordConfiguration : IEntityTypeConfiguration<Landlord>
{
    public void Configure(EntityTypeBuilder<Landlord> builder)
    {
        

        builder.HasKey(l => l.Id);

        builder.Property(l => l.CompanyName)
               .HasMaxLength(255)
               .IsRequired(false);

        builder.Property(l => l.Bio)
               .HasColumnType("text")
               .IsRequired(false);

        builder.HasOne(rp => rp.User)
               .WithOne(r => r.Landlord)
               .HasForeignKey<Landlord>(l => l.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
