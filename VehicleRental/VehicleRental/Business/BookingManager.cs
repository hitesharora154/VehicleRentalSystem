using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRental.Models;
using VehicleRental.Repository;

namespace VehicleRental.Business
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository BookingRepository;

        public BookingManager(IBookingRepository bookingRepository)
        {
            BookingRepository = bookingRepository;
        }

        public async Task<int> AddBooking(ApplicationDbContext dbContext, Booking booking)
        {
            booking.ID = Guid.NewGuid();
            return await BookingRepository.AddAsync(dbContext, booking);
        }

        public async Task<int> DeleteBooking(ApplicationDbContext dbContext, Guid id)
        {
            return await BookingRepository.DeleteAsync(dbContext, id);
        }

        public IEnumerable<Booking> GetAllBookings(ApplicationDbContext dbContext, string userId = null)
        {
            return BookingRepository.GetAll(dbContext, userId);
        }
    }
}
