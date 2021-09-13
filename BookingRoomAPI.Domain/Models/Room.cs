using BookingRoomAPI.Domain.Models.Base;
using BookingRoomAPI.Domain.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingRoomAPI.Domain.Models
{
    public class Room : EntityBase
    {
        public int Number { get; set; }
        public string Description { get; set; }
        public RoomType Type { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
