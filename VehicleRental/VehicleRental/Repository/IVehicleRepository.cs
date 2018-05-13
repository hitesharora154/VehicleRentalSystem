using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRental.Models;

namespace VehicleRental.Repository
{
    public interface IVehicleRepository
    {
        Task<int> AddAsync(Vehicle vehicle, ApplicationDbContext dbContext);

        IEnumerable<Vehicle> GetAll(ApplicationDbContext dbContext, Guid vehicleTypeId);
    }
}
