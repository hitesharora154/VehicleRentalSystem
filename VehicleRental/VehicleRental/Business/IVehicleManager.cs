using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRental.Models;

namespace VehicleRental.Business
{
    public interface IVehicleManager
    {
        Task<int> AddVehicle(Vehicle vehicle, ApplicationDbContext dbContext);

        IEnumerable<Vehicle> GetAllVehicles(ApplicationDbContext dbContext, Guid vehicleTypeId);
    }
}
