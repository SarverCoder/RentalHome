using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalHome.Core.Entities;

namespace RentalHome.DataAccess.Persistence.Configurations;

public class LandlordConfiguration : IEntityTypeConfiguration<Landlord>
{
    public void Configure(EntityTypeBuilder<Landlord> builder)
    {
        builder.ToTable("Landlords");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.CompanyName)
               .HasMaxLength(255)
               .IsRequired(false);

        builder.Property(l => l.Bio)
               .HasColumnType("text")
               .IsRequired(false);

        builder.Property(l => l.IsVerified)
               .IsRequired();

        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(l => l.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
