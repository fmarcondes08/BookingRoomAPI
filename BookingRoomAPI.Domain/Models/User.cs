using BookingRoomAPI.Domain.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingRoomAPI.Domain.Models
{
    public class User : EntityBase
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
