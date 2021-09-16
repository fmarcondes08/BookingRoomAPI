using BookingRoomAPI.Domain.Models;
using BookingRoomAPI.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoomAPI.Infrastructure.Mappings
{
    public class UserMapping : BaseEntityMapping<User>, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            BaseConfigure(builder);

            builder.ToTable("Users");

            builder.Property(u => u.FullName)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(fk => fk.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
