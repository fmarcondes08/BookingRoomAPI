using BookingRoomAPI.Domain.Interfaces;
using BookingRoomAPI.Domain.Models;
using BookingRoomAPI.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingRoomAPI.Infrastructure.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Booking> GetByBookingCode(string code)
        {
            return await DbSet.Include(x=> x.Room).Include(x => x.User).Where(x=> x.Code.Equals(code)).FirstOrDefaultAsync();
        }
    }
}
