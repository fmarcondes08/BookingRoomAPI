using BookingRoomAPI.Domain.Models;
using BookingRoomAPI.Infrastructure.Mappings.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoomAPI.Infrastructure.Mappings
{
    public class RoomMapping : BaseEntityMapping<Room>, IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            BaseConfigure(builder);

            builder.ToTable("Rooms");

            builder.Property(r => r.Number)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(r => r.Description)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            builder.Property(r => r.Type)
                .HasConversion<string>()
                .IsRequired();
        }
    }
}
