using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRental.Models;

namespace VehicleRental.Business
{
    public interface IBookingManager
    {
        IEnumerable<Booking> GetAllBookings(ApplicationDbContext dbContext, string userId = default(string));

        Task<int> AddBooking(ApplicationDbContext dbContext, Booking booking);

        Task<int> DeleteBooking(ApplicationDbContext dbContext, Guid id);
    }
}
