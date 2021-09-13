using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
