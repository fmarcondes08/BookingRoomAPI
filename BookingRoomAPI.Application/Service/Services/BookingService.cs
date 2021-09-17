using AutoMapper;
using BookingRoomAPI.Application.Dtos.Booking;
using BookingRoomAPI.Application.Helpers;
using BookingRoomAPI.Application.Service.Interfaces;
using BookingRoomAPI.Domain.Interfaces;
using BookingRoomAPI.Domain.Models;
using BookingRoomAPI.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingRoomAPI.Application.Exceptions;

namespace BookingRoomAPI.Application.Service.Services
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomService _roomservice;
        private readonly IUserService _userService;

        public BookingService(IMapper mapper, IBookingRepository bookingRepository, IRoomService roomservice,
            IUserService userService)
        {
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _roomservice = roomservice;
            _userService = userService;
        }

        public async Task<bool> CheckAvailableAsync(DateTime firstDay, DateTime lastDay)
        {
            if (firstDay.Equals(DateTime.MinValue)) throw new Exception("First Day can't be null or empty");

            if (lastDay.Equals(DateTime.MinValue)) throw new Exception("Last Day can't be null or empty");

            if (firstDay > lastDay) throw new Exception("Check the range date");

            if (!Utils.VerifyDateRange(firstDay, lastDay)) throw new ValidateExceptions("Invalid Range of Date");

            var room = await _roomservice.GetAvailableRoom(firstDay, lastDay);

            return room == null ? false : true;
        }

        public async Task<BookingOutputDto> CreateAsync(CreateBookingInputDto bookingInputDto)
        {
            if (string.IsNullOrEmpty(bookingInputDto.Email)) throw new Exception("Email can't be null or empty");

            if (!Utils.VerifyDateRange(bookingInputDto.CheckIn, bookingInputDto.CheckOut)) throw new ValidateExceptions("Invalid Range of Date");

            var booking = _mapper.Map<Booking>(bookingInputDto);
            var userDto = _mapper.Map<User>(bookingInputDto);

            var room = await _roomservice.GetAvailableRoom(bookingInputDto.CheckIn, bookingInputDto.CheckOut, null);
            var user = await _userService.GetByEmail(bookingInputDto.Email);

            booking.RoomId = new Guid(room.Id);

            if (user == null)
            {
                booking.User = userDto;
            }
            else
            {
                booking.UserId = new Guid(user.Id);
            }

            booking.Status = BookingStatus.Booked;
            booking.CheckIn = bookingInputDto.CheckIn.Date;
            booking.CheckOut = bookingInputDto.CheckOut.Date.AddDays(1).AddSeconds(-1);
            booking.Code = Utils.GenerateCode(6);

            booking = await _bookingRepository.Add(booking);

            booking.Room = _mapper.Map<Room>(room);
            booking.User = user == null ? booking.User : _mapper.Map<User>(user);

            return _mapper.Map<BookingOutputDto>(booking);
        }

        public async Task<BookingOutputDto> UpdateAsync(UpdateBookingInputDto bookingInputDto)
        {
            if (string.IsNullOrEmpty(bookingInputDto.code)) throw new Exception("Code can`t be null or empty");

            if (!Utils.VerifyDateRange(bookingInputDto.CheckIn, bookingInputDto.CheckOut)) throw new ValidateExceptions("Invalid Range of Date");

            var booking = await _bookingRepository.GetByBookingCode(bookingInputDto.code);

            if (booking == null) throw new Exception("Booking does not found");

            var room = await _roomservice.GetAvailableRoom(bookingInputDto.CheckIn, bookingInputDto.CheckOut, bookingInputDto.code);

            booking.RoomId = new Guid(room.Id);
            booking.CheckIn = bookingInputDto.CheckIn.Date;
            booking.CheckOut = bookingInputDto.CheckOut.Date.AddDays(1).AddSeconds(-1);

            booking = await _bookingRepository.Update(booking);

            return _mapper.Map<BookingOutputDto>(booking);
        }

        public async Task<bool> CancelAsync(string code)
        {
            var booking = await GetByBookingCode(code);

            booking.Status = BookingStatus.Canceled;

            booking = await _bookingRepository.Update(booking);

            return booking.Status == BookingStatus.Canceled;
        }

        public async Task<BookingOutputDto> GetByCode(string code)
        {
            var booking = await GetByBookingCode(code);

            return _mapper.Map<BookingOutputDto>(booking);
        }

        public async Task<List<BookingOutputDto>> GetListBookings(DateTime firstDay, DateTime lastDay)
        {
            if (firstDay.Equals(DateTime.MinValue)) throw new Exception("First Day can't be null or empty");

            if (lastDay.Equals(DateTime.MinValue)) throw new Exception("Last Day can't be null or empty");

            if (firstDay > lastDay) throw new Exception("Check the range date");

            var listBookings = await _bookingRepository.GetAll(x => x.CheckIn >= firstDay.Date && x.CheckOut <= lastDay.Date.AddDays(1).AddSeconds(-1), "Room,User");

            return _mapper.Map<List<BookingOutputDto>>(listBookings);
        }

        #region Private Methods

        private async Task<Booking> GetByBookingCode(string code)
        {
            if (string.IsNullOrEmpty(code)) throw new ValidateExceptions("Code can`t be null or empty");

            var booking = await _bookingRepository.GetByBookingCode(code);

            if (booking == null) throw new Exception("Booking not found");

            return booking;
        }

        #endregion
    }
}
