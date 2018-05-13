using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRental.Models;

namespace VehicleRental.Repository
{
    public class BookingRepository : IBookingRepository
    {
        public async Task<int> AddAsync(ApplicationDbContext dbContext, Booking booking)
        {
            using (dbContext)
            {

                var checkBooking = dbContext.Bookings.FirstOrDefault(b => b.BookingDate.Equals(booking.BookingDate) && b.VehicleID.Equals(booking.VehicleID));

                if (checkBooking == null)
                {
                    var response = await dbContext.Bookings.AddAsync(booking);
                    int result = (response.State == EntityState.Added) ? 1 : 0;
                    await dbContext.SaveChangesAsync();

                    return result;
                }

                return -1;
            }
        }

        public async Task<int> DeleteAsync(ApplicationDbContext dbContext, Guid id)
        {
            using (dbContext)
            {
                var booking = await dbContext.Bookings.FirstOrDefaultAsync(b => b.ID.Equals(id));

                if(booking != null)
                {
                    var deletionResponse = dbContext.Remove(booking);
                    int response = deletionResponse.State == EntityState.Deleted ? 1 : 0;
                    await dbContext.SaveChangesAsync();

                    return response;
                }
                else
                {
                    return 0;
                }
            }
        }

        public IEnumerable<Booking> GetAll(ApplicationDbContext dbContext, string userId = null)
        {
            using (dbContext)
            {
                IEnumerable<Booking> bookingList = Enumerable.Empty<Booking>();

                if (userId == null || userId.Equals(default(string)))
                {
                    bookingList = dbContext.Bookings.Include(b => b.User).Include(b => b.Vehicle).ThenInclude(v => v.VehicleType).Where(b => b.BookingDate > DateTime.Now).ToList();
                }
                else
                {
                    bookingList = dbContext.Bookings.Include(b => b.Vehicle).ThenInclude(v => v.VehicleType).Where(b => b.UserID.Equals(userId) && b.BookingDate > DateTime.Now).ToList();
                }

                return bookingList;
            }
        }
    }
}
