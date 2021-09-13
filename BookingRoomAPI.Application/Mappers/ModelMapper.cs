using AutoMapper;
using BookingRoomAPI.Application.Dtos.Booking;
using BookingRoomAPI.Application.Dtos.Room;
using BookingRoomAPI.Application.Dtos.User;
using BookingRoomAPI.Domain.Models;

namespace BookingRoomAPI.Application.Mappers
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<User, UserOutputDto>();

            CreateMap<Booking, BookingOutputDto>();

            CreateMap<Room, RoomOutputDto>();
        }
    }
}
