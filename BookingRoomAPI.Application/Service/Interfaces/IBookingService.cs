using BookingRoomAPI.Application.Dtos.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRoomAPI.Application.Service.Interfaces
{
    public interface IBookingService
    {
        /// <summary>
        /// Check if has any room available
        /// </summary>
        /// <param name="firstDay">First Day</param>
        /// <param name="lastDay">Last Day</param>
        /// <returns>True/False if Available or not</returns>
        Task<bool> CheckAvailableAsync(DateTime firstDay, DateTime lastDay);
        /// <summary>
        /// Create a new booking
        /// </summary>
        /// <param name="bookingInputDto">Booking Data</param>
        /// <returns>Booking Created</returns>
        Task<BookingOutputDto> CreateAsync(CreateBookingInputDto bookingInputDto);
        /// <summary>
        /// Update a Booking
        /// </summary>
        /// <param name="bookingInputDto">Booking Data</param>
        /// <returns>Booking Updated</returns>
        Task<BookingOutputDto> UpdateAsync(UpdateBookingInputDto bookingInputDto);
        /// <summary>
        /// Cancel a booking by code
        /// </summary>
        /// <param name="code">Booking code</param>
        /// <returns>True/False if cancelled or not</returns>
        Task<bool> CancelAsync(string code);
        /// <summary>
        /// Get a Booking by code
        /// </summary>
        /// <param name="code">Booking code</param>
        /// <returns>Booking</returns>
        Task<BookingOutputDto> GetByCode(string code);
        /// <summary>
        /// Get a list of Bookings
        /// </summary>
        /// <param name="firstDay">First Date</param>
        /// <param name="lastDay">Last Date</param>
        /// <returns>List of Bookings</returns>
        Task<List<BookingOutputDto>> GetListBookings(DateTime firstDay, DateTime lastDay);

    }
}
