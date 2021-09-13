using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using BookingRoomAPI.Application.Dtos.Booking;

namespace BookingRoomAPI.Test
{
    /// <summary>
    /// For automated testing, data must be previously inserted into the database
    /// </summary>
    public class BookingApplicationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public BookingApplicationTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void ShouldGetByCode()
        {
            //Arrange
            var client = _factory.CreateClient();

            // Act
            var responseList = await client.GetAsync($"/booking/GetListBookings?firstDay=2021-01-01&lastDay=2021-12-31");
            var jsonResultList = responseList.Content.ReadAsStringAsync().Result;
            var reservationList = JsonConvert.DeserializeObject<List<BookingOutputDto>>(jsonResultList);

            var response = await client.GetAsync($"/booking/GetByCode?code={reservationList.FirstOrDefault().Code}");
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var reservation = JsonConvert.DeserializeObject<BookingOutputDto>(jsonResult);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(reservationList.FirstOrDefault().Code, reservation.Code);
        }

        [Fact]
        public async void ShouldGetListBookings()
        {
            //Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/booking/GetListBookings?firstDay=2021-09-01&lastDay=2021-09-30");

            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var booking = JsonConvert.DeserializeObject<List<BookingOutputDto>>(jsonResult);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.True(booking.Count > 0);
        }

        [Fact]
        public async void ShouldCheckAvailable()
        {
            //Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/booking/CheckAvailable?firstDay={DateTime.Now.AddDays(10).Date.ToShortDateString()}&lastDay={DateTime.Now.AddDays(12).Date.ToShortDateString()}");

            var jsonResult = response.Content.ReadAsStringAsync().Result;

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("true", jsonResult);
        }

        [Fact]
        public async void ShouldCancel()
        {
            //Arrange
            var client = _factory.CreateClient();

            // Act
            var responseList = await client.GetAsync($"/booking/GetListBookings?firstDay=2021-09-01&lastDay=2021-09-30");
            var jsonResultList = responseList.Content.ReadAsStringAsync().Result;
            var bookingList = JsonConvert.DeserializeObject<List<BookingOutputDto>>(jsonResultList);

            var stringContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("code", bookingList.FirstOrDefault().Code),
            });

            var response = await client.PatchAsync($"/booking/Cancel?code={bookingList.FirstOrDefault().Code}", stringContent);
            var jsonResult = response.Content.ReadAsStringAsync().Result;

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("true", jsonResult);
        }
    }
}
