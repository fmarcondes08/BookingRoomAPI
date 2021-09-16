using BookingRoomAPI.Domain.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingRoomAPI.Domain.Models
{
    [Table("Users")]
    public class User : EntityBase
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
