using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRental.Models;

namespace VehicleRental.Repository
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll(ApplicationDbContext dbContext, string userId = default(string));

        Task<int> AddAsync(ApplicationDbContext dbContext, Booking booking);

        Task<int> DeleteAsync(ApplicationDbContext dbContext, Guid id);
    }
}
