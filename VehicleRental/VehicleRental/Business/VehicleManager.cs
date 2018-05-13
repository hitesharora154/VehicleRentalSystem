using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRental.Models;
using VehicleRental.Repository;

namespace VehicleRental.Business
{
    public class VehicleManager : IVehicleManager
    {
        private readonly IVehicleRepository VehicleRepository;

        private readonly IVehicleTypeRepository VehicleTypeRepository;

        public VehicleManager(IVehicleRepository vehicleRepository, IVehicleTypeRepository vehicleTypeRepository)
        {
            VehicleRepository = vehicleRepository;
            VehicleTypeRepository = vehicleTypeRepository;
        }

        public async Task<int> AddVehicle(Vehicle vehicle, ApplicationDbContext dbContext)
        {
            vehicle.ID = Guid.NewGuid();
            return await VehicleRepository.AddAsync(vehicle, dbContext);
        }

        public IEnumerable<Vehicle> GetAllVehicles(ApplicationDbContext dbContext, Guid vehicleTypeId)
        {
            return VehicleRepository.GetAll(dbContext, vehicleTypeId);
        }
    }
}
