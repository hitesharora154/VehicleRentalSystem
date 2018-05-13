using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using VehicleRental.Models.Identity;
using VehicleRental.Models.Responses;
using VehicleRental.Models.ViewModels;

namespace VehicleRental.Business
{
    public class TokenManager : ITokenManager
    {
        private readonly IConfiguration Configuration;

        private readonly UserManager<ApplicationUser> UserManager;

        private readonly SignInManager<ApplicationUser> SignInManager;

        private readonly RoleManager<ApplicationRole> RoleManager;

        public TokenManager(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            Configuration = configuration;
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public async Task<TokenResponse> BuildToken(LoginViewModel loginViewModel)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var appUser = await UserManager.FindByEmailAsync(loginViewModel.Email);

            if (appUser != null)
            {
                if (await IsAuthenticated(appUser, loginViewModel.Password))
                {
                    var claims = await GetClaimsAsync(appUser);
                    var token = new JwtSecurityToken(
                            Configuration["Jwt:Issuer"],
                            Configuration["Jwt:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(3),
                            signingCredentials: creds
                    );

                    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                    return new TokenResponse
                    {
                        Token = jwt,
                        Name = appUser.Name,
                        ValidUpto = DateTime.Now.AddHours(3),
                        Role = UserManager.GetRolesAsync(appUser).Result.FirstOrDefault(),
                        UserID = appUser.Id
                    };
                }
            }

            return null;
        }

        private async Task<IList<Claim>> GetClaimsAsync(ApplicationUser applicationUser)
        {
            var userClaims = await UserManager.GetClaimsAsync(applicationUser);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Name));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, applicationUser.Email));

            var userRoles = await UserManager.GetRolesAsync(applicationUser);

            foreach (var role in userRoles)
            {
                userClaims.Add(new Claim("roles", role));
                var userRole = await RoleManager.FindByNameAsync(role);

                if (userRole != null)
                {
                    var roleClaims = await RoleManager.GetClaimsAsync(userRole);
                    foreach (var roleClaim in roleClaims)
                    {
                        userClaims.Add(roleClaim);
                    }
                }
            }

            return userClaims;
        }

        private async Task<bool> IsAuthenticated(ApplicationUser user, string password)
        {
            var response = await SignInManager.PasswordSignInAsync(user, password, true, false);

            return response.Succeeded;
        }
    }
}
