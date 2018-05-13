using System.Threading.Tasks;
using VehicleRental.Models.Responses;
using VehicleRental.Models.ViewModels;

namespace VehicleRental.Business
{
    public interface ITokenManager
    {
        Task<TokenResponse> BuildToken(LoginViewModel loginViewModel);
    }
}
