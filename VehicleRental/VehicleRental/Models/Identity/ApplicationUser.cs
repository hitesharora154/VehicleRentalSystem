
using Microsoft.AspNetCore.Identity;

namespace VehicleRental.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
