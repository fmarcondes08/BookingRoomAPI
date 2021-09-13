using System;
using System.Collections.Generic;
using System.Text;

namespace BookingRoomAPI.Application.Dtos.User
{
    public class UpdateUserInputDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
