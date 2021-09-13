using BookingRoomAPI.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BookingRoomAPI.Infrastructure.Mappings.Base
{
    public abstract class BaseEntityMapping<T> where T : EntityBase
    {
        public void BaseConfigure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasDefaultValueSql("newsequentialid()");

            builder.Property(c => c.Active)
                .HasColumnType("bit")
                .HasConversion<bool>()
                .IsRequired();

            builder.Property(c => c.Created_At)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.Deleted_At)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(c => c.Updated_At)
                .HasColumnType("datetime")
                .IsRequired(false);
        }
    }
}
