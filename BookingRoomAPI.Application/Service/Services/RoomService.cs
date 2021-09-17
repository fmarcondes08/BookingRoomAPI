using AutoMapper;
using BookingRoomAPI.Application.Dtos.Room;
using BookingRoomAPI.Application.Exceptions;
using BookingRoomAPI.Application.Service.Interfaces;
using BookingRoomAPI.Domain.Interfaces;
using BookingRoomAPI.Domain.Models;
using System;
using System.Threading.Tasks;

namespace BookingRoomAPI.Application.Service.Services
{
    public class RoomService : IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;

        public RoomService(IMapper mapper, IRoomRepository roomRepository)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
        }

        public async Task<RoomOutputDto> CreateAsync(CreateRoomInputDto roomDto)
        {
            var roomMap = _mapper.Map<Room>(roomDto);

            var roomCreated = await _roomRepository.Add(roomMap);

            return _mapper.Map<RoomOutputDto>(roomCreated);
        }

        public async Task<RoomOutputDto> UpdateAsync(UpdateRoomInputDto roomDto)
        {
            var roomMap = _mapper.Map<Room>(roomDto);

            var roomCreated = await _roomRepository.Update(roomMap);

            return _mapper.Map<RoomOutputDto>(roomCreated);
        }

        public async Task<RoomOutputDto> GetAvailableRoom(DateTime checkIn, DateTime checkOut, string code)
        {
            var room = await _roomRepository.GetAvailableRoom(checkIn.Date, checkOut.Date.AddDays(1).AddSeconds(-1), code);

            if (room == null) throw new ValidateExceptions("Room Unavailable");

            return _mapper.Map<RoomOutputDto>(room);
        }
    }
}
