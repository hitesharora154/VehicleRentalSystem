using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using VehicleRental.Business;
using VehicleRental.Models;

namespace VehicleRental
{
    [EnableCors("CorsPolicy")]
    [Route("api/booking")]
    public class BookingService : Controller
    {
        private readonly IBookingManager BookingManager;

        private readonly ApplicationDbContext DbContext;

        public BookingService(IBookingManager bookingManager, ApplicationDbContext dbContext)
        {
            BookingManager = bookingManager;
            DbContext = dbContext;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody]Booking booking)
        {
            if (booking == null)
            {
                return BadRequest("Invalid Model");
            }
            if (string.IsNullOrEmpty(booking.UserID) || Guid.Empty.Equals(booking.VehicleID))
            {
                return BadRequest("Invalid Model Data");
            }

            return Ok(await BookingManager.AddBooking(DbContext, booking));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
        [Route("customer")]
        [HttpGet]
        public IActionResult GetBookings(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest("User Id invalid");
            }

            return Ok(BookingManager.GetAllBookings(DbContext, userID));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Route("all")]
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            return Ok(BookingManager.GetAllBookings(DbContext));
        }

        [HttpDelete]
        public IActionResult DeleteBooking(Guid id)
        {
            if (Guid.Empty.Equals(id))
            {
                return BadRequest("Invalid Booking id");
            }

            return Ok(BookingManager.DeleteBooking(DbContext, id));
        }
    }
}
