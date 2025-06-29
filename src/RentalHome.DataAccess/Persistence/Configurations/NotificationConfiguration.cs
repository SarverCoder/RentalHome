using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalHome.Core.Entities;

namespace RentalHome.DataAccess.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(r => r.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(p => p.Content)
            .IsRequired();

        builder
            .Property(n => n.IsRead)
            .HasDefaultValue(false);



    }
}