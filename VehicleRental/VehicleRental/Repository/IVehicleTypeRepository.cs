using System.Collections.Generic;
using VehicleRental.Models;

namespace VehicleRental.Repository
{
    public interface IVehicleTypeRepository
    {
        IEnumerable<VehicleType> GetAll(ApplicationDbContext dbContext);
    }
}
