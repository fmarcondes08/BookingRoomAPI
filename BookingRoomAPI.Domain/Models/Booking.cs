using BookingRoomAPI.Domain.Models.Base;
using BookingRoomAPI.Domain.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingRoomAPI.Domain.Models
{
    [Table("Bookings")]
    public class Booking : EntityBase
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        public string Code { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public BookingStatus Status { get; set; }
    }
}
