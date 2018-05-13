using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRental.Business;
using VehicleRental.Models.ViewModels;

namespace VehicleRental
{
    [EnableCors("CorsPolicy")]
    [Route("api/token")]
    public class TokenService : Controller
    {
        private readonly ITokenManager TokenManager;

        public TokenService(ITokenManager tokenManager)
        {
            TokenManager = tokenManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody]LoginViewModel loginViewModel)
        {
            if (loginViewModel == null)
            {
                return BadRequest("Invalid Model");
            }

            if (string.IsNullOrEmpty(loginViewModel.Email) || string.IsNullOrEmpty(loginViewModel.Password))
            {
                return BadRequest("Invalid Model Data");
            }

            var tokenResponse = await TokenManager.BuildToken(loginViewModel);

            if (tokenResponse == null)
            {
                return Unauthorized();
            }

            return Ok(tokenResponse);
        }


    }
}
