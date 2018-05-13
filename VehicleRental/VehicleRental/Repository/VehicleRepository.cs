using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRental.Models;

namespace VehicleRental.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        public async Task<int> AddAsync(Vehicle vehicle, ApplicationDbContext dbContext)
        {
            using (dbContext)
            {
                var response = await dbContext.Vehicles.AddAsync(vehicle);
                int result = (response.State == EntityState.Added) ? 1 : 0;
                await dbContext.SaveChangesAsync();

                return result;
            }
        }

        public IEnumerable<Vehicle> GetAll(ApplicationDbContext dbContext, Guid vehicleTypeId)
        {
            using (dbContext)
            {
                return dbContext.Vehicles.Include(v => v.VehicleType).Where(v => v.TypeID.Equals(vehicleTypeId)).ToList();
            }
        }
    }
}
