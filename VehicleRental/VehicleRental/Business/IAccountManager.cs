using System.Threading.Tasks;
using VehicleRental.Models.ViewModels;

namespace VehicleRental.Business
{
    public interface IAccountManager
    {
        Task<int> AddUser(RegisterViewModel registerViewModel);
    }
}
