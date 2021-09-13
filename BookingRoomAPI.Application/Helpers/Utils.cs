using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingRoomAPI.Application.Helpers
{
    public static class Utils
    {
        /// <summary>
        /// Generate a random string according the length of the string
        /// </summary>
        /// <param name="length">Number of characters</param>
        /// <returns>Return a random string</returns>
        public static string GenerateCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        /// <summary>
        ///  Verify Booking date range
        /// </summary>
        /// <param name="firstDay">First Date</param>
        /// <param name="lastDay">Last Date</param>
        /// <returns>True: Is Valid | False: Not Valid</returns>
        public static bool VerifyDateRange(DateTime firstDay, DateTime lastDay)
        {
            return firstDay.Date >= DateTime.Now.AddDays(1).Date && //Start reservation at least next day of booking
                   (lastDay.Date.AddDays(1).AddSeconds(-1) - firstDay.Date) <= TimeSpan.FromDays(3) && //Stay period can't be longer than 3 days
                   firstDay.Date <= DateTime.Now.AddDays(30).Date; //Can't be reserved more than 30 days in advanced;
        }
    }
}
