using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VehicleRental.Business;
using VehicleRental.Models;

namespace VehicleRental
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Customer")]
    [EnableCors("CorsPolicy")]
    [Route("api/vehicle")]
    public class VehicleService : Controller
    {
        private readonly ApplicationDbContext DbContext;

        private readonly IVehicleManager VehicleManager;

        public VehicleService(ApplicationDbContext dbContext, IVehicleManager vehicleManager)
        {
            DbContext = dbContext;
            VehicleManager = vehicleManager;
        }

        //[ResponseCache(VaryByHeader ="User-Agent", Duration = 60)]
        [ETagFilter(200)]
        [HttpGet]
        public IActionResult GetAll(Guid vehicleTypeId)
        {
            return Ok(VehicleManager.GetAllVehicles(DbContext, vehicleTypeId));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return BadRequest("Invalid Vehicle Model");
            }
            if (vehicle.Name.Equals(string.Empty) || vehicle.TypeID.Equals(Guid.Empty))
            {
                return BadRequest("Invalid Data");
            }

            return Ok(await VehicleManager.AddVehicle(vehicle, DbContext));
        }
    }
}
