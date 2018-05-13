using System.Collections.Generic;
using VehicleRental.Models;
using VehicleRental.Repository;

namespace VehicleRental.Business
{
    public class VehicleTypeManager : IVehicleTypeManager
    {
        private readonly IVehicleTypeRepository VehicleTypeRepository;

        public VehicleTypeManager(IVehicleTypeRepository vehicleTypeRepository)
        {
            VehicleTypeRepository = vehicleTypeRepository;
        }

        public IEnumerable<VehicleType> GetAllTypes(ApplicationDbContext dbContext)
        {
            return VehicleTypeRepository.GetAll(dbContext);
        }
    }
}
