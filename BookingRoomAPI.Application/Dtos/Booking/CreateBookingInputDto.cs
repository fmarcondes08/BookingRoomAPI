using System;
using System.ComponentModel.DataAnnotations;

namespace BookingRoomAPI.Application.Dtos.Booking
{
    public class CreateBookingInputDto
    {
        [Required]
        public string FullName { get; set; }  
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut { get; set; }
    }
}
