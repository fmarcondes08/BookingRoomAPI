using BookingRoomAPI.Application.Dtos.Booking;
using BookingRoomAPI.Application.Exceptions;
using BookingRoomAPI.Application.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookingRoomAPI.Controllers
{
    [ApiController]
    [Route("booking")]
    [Produces("application/json")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Checkthe availability of a booking by range of date
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     HttpGet /booking/CheckAvailable
        ///         {URL}/CheckAvailable?firstDay=2021-09-01&lastDay=2021-09-03
        ///     
        /// </remarks>
        /// <param name="firstDay">First Date</param>
        /// <param name="lastDay">Last Date</param>
        /// <returns>True or False if a Booking is available or not</returns>
        /// <response code="200">Returns a boolean indicate if a booking is available</response>
        /// <response code="422">Returns error</response> 
        /// <response code="500">Returns Internal Server error</response>
        [HttpGet("CheckAvailable")]
        public async Task<IActionResult> CheckAvailable([FromQuery][Required] DateTime firstDay, [FromQuery][Required] DateTime lastDay)
        {
            try
            {
                var result = await _bookingService.CheckAvailableAsync(firstDay, lastDay);

                Response.StatusCode = StatusCodes.Status200OK;
                return new JsonResult(result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return new JsonResult(new { Errors = ex.Errors.Select(x => x.ErrorMessage) });
            }
            catch (ValidateExceptions ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return new JsonResult(new { Errors = ex.Message });
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { Errors = new string[] { ex.Message } });
            }
        }

        /// <summary>
        /// Search for booking by code
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     HttpGet /booking/GetByCode
        ///         {URL}/code?code=ABC123
        ///     
        /// </remarks>
        /// <param name="code">Booking code</param>
        /// <returns>Booking</returns>
        /// <response code="200">Returns Booking</response>
        /// <response code="422">Returns error</response> 
        /// <response code="500">Returns Internal Server error</response> 
        [HttpGet("GetByCode")]
        public async Task<IActionResult> GetByCode([FromQuery][Required] string code)
        {
            try
            {
                var result = await _bookingService.GetByCode(code);

                Response.StatusCode = StatusCodes.Status200OK;
                return new JsonResult(result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return new JsonResult(new { Errors = ex.Errors.Select(x => x.ErrorMessage) });
            }
            catch (ValidateExceptions ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return new JsonResult(new { Errors = ex.Message });
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { Errors = new string[] { ex.Message } });
            }
        }

        /// <summary>
        /// Get a list of bookings by a range of dates
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     HttpGet /booking/GetListBookings
        ///         {URL}/GetListBookings?firstDay=2021-09-01&lastDay=2021-09-03
        ///     
        /// </remarks>
        /// <param name="firstDay">First date</param>
        /// <param name="lastDay">Last date</param>
        /// <returns>List of bookings</returns>
        /// <response code="200">Returns a list of bookings</response>
        /// <response code="422">Returns error</response> 
        /// <response code="500">Returns Internal Server error</response> 
        [HttpGet("GetListBookings")]
        public async Task<IActionResult> GetListBookings([FromQuery][Required] DateTime firstDay, [FromQuery][Required] DateTime lastDay)
        {
            try
            {
                var result = await _bookingService.GetListBookings(firstDay, lastDay);

                Response.StatusCode = StatusCodes.Status200OK;
                return new JsonResult(result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return new JsonResult(new { Errors = ex.Errors.Select(x => x.ErrorMessage) });
            }
            catch (ValidateExceptions ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return new JsonResult(new { Errors = ex.Message });
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { Errors = new string[] { ex.Message } });
            }
        }

        /// <summary>
        /// Booking a Room
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /booking/Create
        ///     {
        ///         "CheckIn": "2021-09-01",
        ///         "CheckOut": "2021-09-02",
        ///         "FullName": "Fabricio Marcondes",
        ///         "Email": "fabricio.marcondes@email.ca"
        ///     }
        ///     
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns>Created Booking</returns>
        /// <response code="200">Success</response>
        /// <response code="201">Returns Created Booking</response>
        /// <response code="422">Returns error</response> 
        /// <response code="500">Returns Internal Server error</response> 
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingInputDto dto)
        {
            try
            {
                var result = await _bookingService.CreateAsync(dto);

                Response.StatusCode = StatusCodes.Status200OK;
                return new JsonResult(result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return new JsonResult(new { Errors = ex.Errors.Select(x => x.ErrorMessage) });
            }
            catch (ValidateExceptions ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return new JsonResult(new { Errors = ex.Message });
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { Errors = new string[] { ex.Message } });
            }
        }

        /// <summary>
        /// Update an existing booking
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /booking/Update
        ///     {
        ///         "code": "ABC123",
        ///         "CheckIn": "2021-09-01",
        ///         "CheckOut": "2021-09-02"
        ///     }
        ///     
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns>Updated Booking</returns>
        /// <response code="200">Returns booking</response>
        /// <response code="422">Returns error</response> 
        /// <response code="500">Returns Internal Server error</response> 
        [HttpPut]
        public async Task<IActionResult> Update(UpdateBookingInputDto dto)
        {
            try
            {
                var result = await _bookingService.UpdateAsync(dto);

                Response.StatusCode = StatusCodes.Status200OK;
                return new JsonResult(result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return new JsonResult(new { Errors = ex.Errors.Select(x => x.ErrorMessage) });
            }
            catch (ValidateExceptions ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return new JsonResult(new { Errors = ex.Message });
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { Errors = new string[] { ex.Message } });
            }
        }

        /// <summary>
        /// Cancel booking using code
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     HttpPatch /booking/Cancel
        ///         {URL}/code?code=ABC123
        ///     
        /// </remarks>
        /// <param name="code">Booking code</param>
        /// <returns>True or False if a booking was canceled or not</returns>
        /// <response code="200">Returns boolean</response>
        /// <response code="422">Returns error</response> 
        /// <response code="500">Returns Internal Server error</response> 
        [HttpPatch("Cancel")]
        public async Task<IActionResult> Cancel([FromQuery][Required] string code)
        {
            try
            {
                var result = await _bookingService.CancelAsync(code);

                Response.StatusCode = StatusCodes.Status200OK;
                return new JsonResult(result);
            }
            catch (FluentValidation.ValidationException ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return new JsonResult(new { Errors = ex.Errors.Select(x => x.ErrorMessage) });
            }
            catch (ValidateExceptions ex)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return new JsonResult(new { Errors = ex.Message });
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new { Errors = new string[] { ex.Message } });
            }
        }
    }
}
