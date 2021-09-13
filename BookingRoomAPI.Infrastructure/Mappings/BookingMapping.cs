using BookingRoomAPI.Domain.Models;
using BookingRoomAPI.Domain.Models.Enums;
using BookingRoomAPI.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoomAPI.Infrastructure.Mappings
{
    public class BookingMapping : BaseEntityMapping<Booking>, IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            BaseConfigure(builder);

            builder.ToTable("Bookings");

            builder.Property(b => b.CheckIn)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(b => b.CheckOut)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(p => p.Code)
                .HasColumnType("nvarchar(6)")
                .HasMaxLength(6)
                .IsRequired();

            builder.Property(b => b.Status)
                .HasDefaultValue(BookingStatus.None)
                .HasConversion<string>()
                .IsRequired();

            builder.HasIndex(p => new { p.Code })
                .IsUnique();

            builder.HasIndex(p => new { p.UserId, p.CheckIn, p.CheckOut })
                .IsUnique();
        }
    }
}
