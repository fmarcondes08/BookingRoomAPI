using BookingRoomAPI.Domain.Interfaces.Base;
using BookingRoomAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingRoomAPI.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">User Email</param>
        /// <returns>User</returns>
        Task<User> GetByEmail(string email);
    }
}
