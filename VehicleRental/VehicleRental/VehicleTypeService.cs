using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VehicleRental.Business;
using VehicleRental.Models;

namespace VehicleRental
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    [Route("api/vehicletype")]
    public class VehicleTypeService : Controller
    {
        private readonly ApplicationDbContext DbContext;

        private readonly IVehicleTypeManager VehicleTypeManager;

        public VehicleTypeService(ApplicationDbContext dbContext, IVehicleTypeManager vehicleTypeManager)
        {
            DbContext = dbContext;
            VehicleTypeManager = vehicleTypeManager;
        }

        [ETagFilter(200)]
        [HttpGet]
        public IActionResult GetAllTypes()
        {
            return Ok(VehicleTypeManager.GetAllTypes(DbContext));
        }
    }
}
