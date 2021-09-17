using BookingRoomAPI.Domain.Interfaces;
using BookingRoomAPI.Domain.Models;
using BookingRoomAPI.Domain.Models.Enums;
using BookingRoomAPI.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRoomAPI.Infrastructure.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Room> GetAvailableRoom(DateTime checkIn, DateTime checkOut, string code = null)
        {
            return await DbSet.Include(b=> b.Bookings)
                .FirstOrDefaultAsync(x => x.Bookings == null || x.Bookings.Any(y=> 
                        y.CheckIn < checkOut.Date
                        && checkIn.Date < y.CheckOut
                        && y.Status.Equals(BookingStatus.Booked)
                        && y.Active
                        && (code == null ? true : y.Code != code) 
                        ) == false);
        }
    }
}
