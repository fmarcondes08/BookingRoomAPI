using BookingRoomAPI.Application.Dtos.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingRoomAPI.Application.Service.Interfaces
{
    public interface IRoomService
    {
        Task<RoomOutputDto> CreateAsync(CreateRoomInputDto roomDto);
        Task<RoomOutputDto> UpdateAsync(UpdateRoomInputDto roomDto);
        Task<RoomOutputDto> GetAvailableRoom(DateTime checkIn, DateTime checkOut, string code = null);
    }
}
