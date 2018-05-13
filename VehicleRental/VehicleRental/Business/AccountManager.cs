using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using VehicleRental.Models.Identity;
using VehicleRental.Models.ViewModels;

namespace VehicleRental.Business
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> UserManager;

        private readonly RoleManager<ApplicationRole> RoleManager;

        public AccountManager(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public async Task<int> AddUser(RegisterViewModel registerViewModel)
        {
            var appUser = new ApplicationUser
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email,
                Name = registerViewModel.Name
            };

            var result = await UserManager.CreateAsync(appUser, registerViewModel.Password);

            var isRoleExist = await RoleManager.RoleExistsAsync(registerViewModel.Role);

            if (!isRoleExist)
            {
                await RoleManager.CreateAsync(new ApplicationRole { Name = registerViewModel.Role });
            }

            appUser = await UserManager.FindByEmailAsync(registerViewModel.Email);

            var roleResponse = await UserManager.AddToRoleAsync(appUser, registerViewModel.Role);
            return roleResponse.Succeeded ? 1 : 0;
        }
    }
}
