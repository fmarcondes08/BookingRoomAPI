using System;
using System.Collections.Generic;
using System.Text;

namespace BookingRoomAPI.Application.Exceptions
{
    public class ValidateExceptions : Exception
    {
        public ValidateExceptions(string message) : base(message)
        {
        }

        public ValidateExceptions(Exception ex) : base(ex.Message)
        {
        }
    }
}
