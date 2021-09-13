using AutoMapper;
using BookingRoomAPI.Application.Dtos.Booking;
using BookingRoomAPI.Application.Dtos.Room;
using BookingRoomAPI.Application.Dtos.User;
using BookingRoomAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingRoomAPI.Application.Mappers
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<UserOutputDto, User>();

            CreateMap<RoomOutputDto, Room>();

            CreateMap<CreateBookingInputDto, Booking>();
            CreateMap<UpdateBookingInputDto, Booking>();

            CreateMap<CreateBookingInputDto, User>();

            CreateMap<CreateBookingInputDto, Booking>().ForMember(x=> x.Code, action => action.Ignore());
        }
    }
}
