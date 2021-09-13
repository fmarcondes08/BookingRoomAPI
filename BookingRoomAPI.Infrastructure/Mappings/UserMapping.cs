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

            builder.Property(p => p.FullName)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(p => p.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(fk => fk.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.Email)
                .IsUnique();
        }
    }
}
