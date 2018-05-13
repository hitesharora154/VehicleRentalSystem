using System.Collections.Generic;
using VehicleRental.Models;

namespace VehicleRental.Business
{
    public interface IVehicleTypeManager
    {
        IEnumerable<VehicleType> GetAllTypes(ApplicationDbContext dbContext);
    }
}
