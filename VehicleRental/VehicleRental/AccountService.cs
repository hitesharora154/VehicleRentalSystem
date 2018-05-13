using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRental.Business;
using VehicleRental.Models.ViewModels;

namespace VehicleRental
{
    [EnableCors("CorsPolicy")]
    [Route("api/account")]
    public class AccountService : Controller
    {
        private readonly IAccountManager AccountManager;

        public AccountService(IAccountManager accountManager)
        {
            AccountManager = accountManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]RegisterViewModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid model or role Info");
            }

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Name))
            {
                return BadRequest("Invalid model data");
            }

            return Ok(await AccountManager.AddUser(user));
        }
    }
}
