using System.Collections.Generic;
using System.Linq;
using VehicleRental.Models;

namespace VehicleRental.Repository
{
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        public IEnumerable<VehicleType> GetAll(ApplicationDbContext dbContext)
        {
            using (dbContext)
            {
                return dbContext.VehicleTypes.ToList();
            }
        }
    }
}
