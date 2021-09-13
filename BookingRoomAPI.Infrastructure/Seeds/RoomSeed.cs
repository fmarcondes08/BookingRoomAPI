using BookingRoomAPI.Domain.Models;
using BookingRoomAPI.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingRoomAPI.Infrastructure.Seeds
{
    public class RoomSeed
    {
        public void Seed(EntityTypeBuilder<Room> builder)
        {
            builder.HasData(
                new Room { Id = Guid.NewGuid(), Number = 1, Description = "Room for Rent", Type = RoomType.Standard, Active = true, Created_At = DateTime.Now, Updated_At = DateTime.Now }
                );
        }
    }
}
