using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using VehicleRental.Models;
using VehicleRental.Repository;

namespace VehicleRental.Business
{
    public class VehicleTypeManager : IVehicleTypeManager
    {
        private readonly IVehicleTypeRepository VehicleTypeRepository;
        private readonly IMemoryCache MemoryCache;

        public VehicleTypeManager(IVehicleTypeRepository vehicleTypeRepository, IMemoryCache memoryCache)
        { 
            VehicleTypeRepository = vehicleTypeRepository;
            MemoryCache = memoryCache;
        }

        public IEnumerable<VehicleType> GetAllTypes(ApplicationDbContext dbContext)
        {
            // need cache expiration policy implemented across system
            if (!(MemoryCache.TryGetValue<IEnumerable<VehicleType>>("vehicleTypes", out IEnumerable<VehicleType> vehicleTypes)))
            {
                vehicleTypes = VehicleTypeRepository.GetAll(dbContext);
                MemoryCache.Set<IEnumerable<VehicleType>>("vehicleTypes", vehicleTypes);
            }
            return vehicleTypes;
        }
    }
}
