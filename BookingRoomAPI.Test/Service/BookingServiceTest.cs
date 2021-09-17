using AutoMapper;
using BookingRoomAPI.Application.Dtos.Booking;
using BookingRoomAPI.Application.Dtos.Room;
using BookingRoomAPI.Application.Dtos.User;
using BookingRoomAPI.Application.Helpers;
using BookingRoomAPI.Application.Service.Interfaces;
using BookingRoomAPI.Application.Service.Services;
using BookingRoomAPI.Domain.Interfaces;
using BookingRoomAPI.Domain.Models;
using BookingRoomAPI.Domain.Models.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace BookingRoomAPI.Test.Service
{
    public class BookingServiceTest
    {
        [Fact]
        public async void Should_Get_By_Code()
        {
            //Initialize models test
            var user = new User
            {
                Id = new Guid(),
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                Email = "user@example.com",
                FullName = "User Test"
            };

            var room = new Room
            {
                Id = new Guid(),
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                Description = "Test Room",
                Number = 1,
                Type = RoomType.Standard
            };

            var code = Utils.GenerateCode(6);
            var booking = new Booking
            {
                Id = new Guid(),
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                Code = code,
                Room = room,
                Status = BookingStatus.Booked,
                User = user
            };

            var bookingOutputDto = new BookingOutputDto
            {
                Active = true,
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                Code = code,
                Room = new Application.Dtos.Room.RoomOutputDto { Id = room.Id.ToString() },
                Status = "Booked",
                User = new Application.Dtos.User.UserOutputDto { Id = user.Id.ToString() }
            };

            // Arrange
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockUserService = new Mock<IUserService>();
            var mockRoomService = new Mock<IRoomService>();
            mockBookingRepository.Setup(r => r.GetByBookingCode(It.IsAny<string>())).Returns(Task.FromResult(booking));
            mockBookingRepository.Setup(r => r.GetByBookingCode(code)).Returns(Task.FromResult(booking));

            mockMapper.Setup(m => m.Map<BookingOutputDto>(booking)).Returns(bookingOutputDto);
            var bookingService = new BookingService(mockMapper.Object, mockBookingRepository.Object, mockRoomService.Object, mockUserService.Object);

            // Act
            var result = await bookingService.GetByCode(code);

            // Assert
            Assert.NotNull(result);
            Assert.Equal<BookingOutputDto>(bookingOutputDto, result);
        }

        [Fact]
        public async void Should_Get_List_Bookings()
        {
            var booking = new Booking
            {
                Id = new Guid(),
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                Code = Utils.GenerateCode(6),
                Room = null,
                Status = BookingStatus.Booked,
                User = null
            };

            var listBooking = new List<Booking>();
            listBooking.Add(booking);

            var listBookingsOutputDto = new List<BookingOutputDto>
            {
                new BookingOutputDto
                {
                    Active = true,
                    CheckIn = booking.CheckIn,
                    CheckOut = booking.CheckOut,
                    Status = "Booked",
                    Code = booking.Code,
                    User = null,
                    Room = null
                }
            };

            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockUserService = new Mock<IUserService>();
            var mockRoomService = new Mock<IRoomService>();
            mockBookingRepository.Setup(r => r.GetAll(It.IsAny<Expression<Func<Booking,bool>>>(), It.IsAny<string>()))
                .Returns(Task.FromResult(listBooking.AsEnumerable()));
            mockMapper.Setup(m => m.Map<List<BookingOutputDto>>(listBooking)).Returns(listBookingsOutputDto);
            var bookingService = new BookingService(mockMapper.Object, mockBookingRepository.Object, mockRoomService.Object, mockUserService.Object);

            // Act
            var result = await bookingService.GetListBookings(booking.CheckIn, booking.CheckOut);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async void Should_Check_Available()
        {
            var roomId = new Guid();
            var booking = new Booking
            {
                Id = new Guid(),
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                Code = Utils.GenerateCode(6),
                RoomId = roomId,
                Status = BookingStatus.Booked,
                User = null
            };

            var listBooking = new List<Booking>();
            listBooking.Add(booking);

            var room = new Room
            {
                Id = roomId,
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                Description = "Test Room",
                Number = 1,
                Type = RoomType.Standard,
                Bookings = listBooking
            };

            var roomOutputDto = new RoomOutputDto
            {
                Id = room.Id.ToString()
            };

            //Arrange
            var mockService = new Mock<IBookingService>();
            var mockMapper = new Mock<IMapper>();
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockUserService = new Mock<IUserService>();
            var mockRoomService = new Mock<IRoomService>();
            mockRoomService.Setup(r => r.GetAvailableRoom(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null))
                .Returns(Task.FromResult(roomOutputDto));
            var bookingService = new BookingService(mockMapper.Object, mockBookingRepository.Object, mockRoomService.Object, mockUserService.Object);

            // Act
            var result = await bookingService.CheckAvailableAsync(DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void Should_Cancel()
        {
            var code = Utils.GenerateCode(6);
            var booking = new Booking
            {
                Id = new Guid(),
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                Code = code,
                Room = null,
                Status = BookingStatus.Booked,
                User = null
            };

            //Arrange
            var mockService = new Mock<IBookingService>();
            var mockMapper = new Mock<IMapper>();
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockUserService = new Mock<IUserService>();
            var mockRoomService = new Mock<IRoomService>();
            mockBookingRepository.Setup(r => r.GetByBookingCode(It.IsAny<string>())).Returns(Task.FromResult(booking));
            mockBookingRepository.Setup(r => r.Update(It.IsAny<Booking>()))
                .Returns(Task.FromResult(booking));
            var bookingService = new BookingService(mockMapper.Object, mockBookingRepository.Object, mockRoomService.Object, mockUserService.Object);

            // Act
            var result = await bookingService.CancelAsync(code);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void Should_Create_Booking()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                Email = "user@example.com",
                FullName = "User Test"
            };

            var userOutputDto = new UserOutputDto
            {
                Id = user.Id.ToString(),
                Active = user.Active,
                Email = user.Email,
                FullName = user.FullName
            };

            var room = new Room
            {
                Id = Guid.NewGuid(),
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                Description = "Test Room",
                Number = 1,
                Type = RoomType.Standard
            };

            var roomOutputDto = new RoomOutputDto
            {
                Id = room.Id.ToString(),
                Active = room.Active,
                Description = room.Description,
                Number = room.Number,
                Type = room.Type.ToString()
            };

            var newBooking = new CreateBookingInputDto
            {
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                Email = user.Email,
                FullName = user.FullName
            };

            var code = Utils.GenerateCode(6);
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                Code = code,
                Room = room,
                Status = BookingStatus.Booked,
                User = user
            };

            var bookingOutputDto = new BookingOutputDto
            {
                Active = true,
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                Code = code,
                Room = new Application.Dtos.Room.RoomOutputDto { Id = room.Id.ToString() },
                Status = "Booked",
                User = new Application.Dtos.User.UserOutputDto { Id = user.Id.ToString() }
            };

            //Arrange
            var mockService = new Mock<IBookingService>();
            var mockMapper = new Mock<IMapper>();
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockUserService = new Mock<IUserService>();
            var mockRoomService = new Mock<IRoomService>();
            mockMapper.Setup(m => m.Map<Booking>(It.IsAny<CreateBookingInputDto>())).Returns(booking);
            mockMapper.Setup(m => m.Map<User>(It.IsAny<CreateBookingInputDto>())).Returns(user);
            mockRoomService.Setup(r => r.GetAvailableRoom(It.IsAny<DateTime>(), It.IsAny<DateTime>(), null))
                .Returns(Task.FromResult(roomOutputDto));
            mockUserService.Setup(u => u.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult(userOutputDto));
            mockBookingRepository.Setup(r => r.Add(It.IsAny<Booking>())).Returns(Task.FromResult(booking));
            mockMapper.Setup(r => r.Map<Room>(It.IsAny<Booking>())).Returns(room);
            mockMapper.Setup(m => m.Map<User>(It.IsAny<User>())).Returns(user);
            mockMapper.Setup(m => m.Map<BookingOutputDto>(It.IsAny<Booking>())).Returns(bookingOutputDto);
            var bookingService = new BookingService(mockMapper.Object, mockBookingRepository.Object, mockRoomService.Object, mockUserService.Object);

            // Act
            var result = await bookingService.CreateAsync(newBooking);

            // Assert
            Assert.Equal<BookingOutputDto>(bookingOutputDto, result);
        }

        [Fact]
        public async void Should_Update_Booking()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                Email = "user@example.com",
                FullName = "User Test"
            };

            var userOutputDto = new UserOutputDto
            {
                Id = user.Id.ToString(),
                Active = user.Active,
                Email = user.Email,
                FullName = user.FullName
            };

            var room = new Room
            {
                Id = Guid.NewGuid(),
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                Description = "Test Room",
                Number = 1,
                Type = RoomType.Standard
            };

            var roomOutputDto = new RoomOutputDto
            {
                Id = room.Id.ToString(),
                Active = room.Active,
                Description = room.Description,
                Number = room.Number,
                Type = room.Type.ToString()
            };

            var code = Utils.GenerateCode(6);

            var updateBooking = new UpdateBookingInputDto
            {
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                code = code
            };

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                Active = true,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now,
                Deleted_At = null,
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                Code = code,
                Room = room,
                Status = BookingStatus.Booked,
                User = user
            };

            var bookingOutputDto = new BookingOutputDto
            {
                Active = true,
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                Code = code,
                Room = new Application.Dtos.Room.RoomOutputDto { Id = room.Id.ToString() },
                Status = "Booked",
                User = new Application.Dtos.User.UserOutputDto { Id = user.Id.ToString() }
            };

            //Arrange
            var mockService = new Mock<IBookingService>();
            var mockMapper = new Mock<IMapper>();
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockUserService = new Mock<IUserService>();
            var mockRoomService = new Mock<IRoomService>();
            mockBookingRepository.Setup(r => r.GetByBookingCode(It.IsAny<string>())).Returns(Task.FromResult(booking));
            mockRoomService.Setup(r => r.GetAvailableRoom(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .Returns(Task.FromResult(roomOutputDto));
            mockBookingRepository.Setup(r => r.Update(It.IsAny<Booking>()))
                .Returns(Task.FromResult(booking));
            mockMapper.Setup(m => m.Map<BookingOutputDto>(It.IsAny<Booking>())).Returns(bookingOutputDto);
            var bookingService = new BookingService(mockMapper.Object, mockBookingRepository.Object, mockRoomService.Object, mockUserService.Object);

            // Act
            var result = await bookingService.UpdateAsync(updateBooking);

            // Assert
            Assert.Equal<BookingOutputDto>(bookingOutputDto, result);
        }
    }
}
