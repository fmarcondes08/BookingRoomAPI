using BookingRoomAPI.Application.Dtos.Room;
using BookingRoomAPI.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingRoomAPI.Application.Dtos.Booking
{
    public class BookingOutputDto
    {
        public string Code { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Status { get; set; }
        public UserOutputDto User { get; set; }
        public RoomOutputDto Room { get; set; }
        public bool Active { get; set; }
    }
}
