using BookingRoomAPI.Domain.Interfaces.Base;
using BookingRoomAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingRoomAPI.Domain.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        /// <summary>
        /// Get Available Room
        /// </summary>
        /// <param name="checkIn">CheckIn Date</param>
        /// <param name="checkOut"> CheckOut Date</param>
        /// <param name="code">Booking Code</param>
        /// <returns>Room</returns>
        Task<Room> GetAvailableRoom(DateTime checkIn, DateTime checkOut, string code = null);
    }
}
