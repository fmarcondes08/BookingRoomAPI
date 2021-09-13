using BookingRoomAPI.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRoomAPI.Application.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserOutputDto> CreateAsync(CreateUserInputDto userDto);
        Task<UserOutputDto> UpdateAsync(UpdateUserInputDto userDto);
        Task<UserOutputDto> GetByEmail(string email);
    }
}
