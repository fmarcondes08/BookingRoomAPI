using BookingRoomAPI.Domain.Interfaces.Base;
using BookingRoomAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingRoomAPI.Domain.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        /// <summary>
        /// Get Booking By Booking Code
        /// </summary>
        /// <param name="code">Booking Code</param>
        /// <returns>Booking</returns>
        Task<Booking> GetByBookingCode(string code);
    }
}
