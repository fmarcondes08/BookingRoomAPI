using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingRoomAPI.Application.Dtos.Booking
{
    public class UpdateBookingInputDto
    {
        [Required]
        public string code { get; set; }
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut { get; set; }
    }
}
